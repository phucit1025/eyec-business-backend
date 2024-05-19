using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;
using EyeC.Domain.Entities;

namespace EyeC.Application.Offices.Commands.CreateOffice;
public record CreateOfficeCommand : IRequest<ResultModel>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string GoogleMapShortLink { get; init; } = string.Empty;
    public string Lat { get; init; } = string.Empty;
    public string Lng { get; init; } = string.Empty;
    public string Introduction { get; init; } = string.Empty;
    public byte[] FeatureImageBytes { get; init; } = Array.Empty<byte>();
}

public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, ResultModel>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public CreateOfficeCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IImageService imageService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ResultModel> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        var transaction = await _dbContext.BeginTransactionAsync();
        try
        {
            var office = new Office()
            {
                Name = request.Name,
                Description = request.Description,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                GoogleMapShortLink = request.GoogleMapShortLink,
                Lat = request.Lat,
                Lng = request.Lng,
                Introduction = request.Introduction
            };
            _dbContext.Offices.Add(office);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.FeatureImageBytes.Length > 0)
            {
                office.FeatureImagePath = await _imageService.SaveImage(request.FeatureImageBytes, $"{Guid.NewGuid()}.jpg", 85);
            }
            _dbContext.Offices.Update(office);
            await _dbContext.SaveChangesAsync(cancellationToken);

            result.Success = true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            result.Success = false;
            result.Message = ex.ToString();
        }
        finally
        {
            transaction.Dispose();
        }
        return result;
    }
}
