using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Offices.Commands.UpdateOffice;
public record UpdateOffice : IRequest<ResultModel>
{
    public int OfficeId { get; init; }
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

public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOffice, ResultModel>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public UpdateOfficeCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IImageService imageService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ResultModel> Handle(UpdateOffice request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        var transaction = await _dbContext.BeginTransactionAsync();
        try
        {
            var office = await _dbContext.Offices.FirstOrDefaultAsync(i => i.OfficeId == request.OfficeId && !i.IsDeleted, cancellationToken);
            if (office == null)
            {
                result.Success = false;
                result.Error = "Office is not existed";
            }
            else
            {
                office.Name = request.Name;
                office.Description = request.Description;
                office.Email = request.Email;
                office.PhoneNumber = request.PhoneNumber;
                office.GoogleMapShortLink = request.GoogleMapShortLink;
                office.Lat = request.Lat;
                office.Lng = request.Lng;
                office.Introduction = request.Introduction;
                _dbContext.Offices.Update(office);
                await _dbContext.SaveChangesAsync(cancellationToken);

                if (request.FeatureImageBytes != null && request.FeatureImageBytes.Length > 0)
                {
                    office.FeatureImagePath = await _imageService.SaveImage(request.FeatureImageBytes, $"{Guid.NewGuid()}.jpg", 85);
                }
                _dbContext.Offices.Update(office);
                await _dbContext.SaveChangesAsync(cancellationToken);

                transaction.Commit();
                result.Success = true;
            }
        }
        catch (Exception ex)
        {

            result.Success = false;
            result.Error = ex.ToString();
        }
        finally
        {
            transaction.Dispose();
        }
        return result;
    }
}
