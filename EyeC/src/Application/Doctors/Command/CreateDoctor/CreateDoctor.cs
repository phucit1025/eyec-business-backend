using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;
using EyeC.Domain.Entities;
using EyeC.Domain.Enums;

namespace EyeC.Application.Doctors.Command.CreateDoctor;
public record CreateDoctorCommand : IRequest<ResultModel>
{
    public string? FullName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public Gender Gender { get; init; }
    public string? PersonalExperience { get; init; }
    public string? Education { get; init; }
    public byte[]? FeatureImageByte { get; init; }
}

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    private readonly IImageService _imageService;

    public CreateDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext, IImageService imageService)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _imageService = imageService;
    }

    public async Task<ResultModel> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        var transaction = await _dbContext.BeginTransactionAsync();
        try
        {
            var newDoctor = new Doctor()
            {
                FullName = request.FullName!,
                PhoneNumber = request.PhoneNumber ?? "",
                Email = request.Email ?? "",
                Gender = request.Gender,
                PersonalExperience = request.PersonalExperience ?? "",
                Education = request.Education ?? ""
            };
            _dbContext.Doctors.Add(newDoctor);

            if(request.FeatureImageByte != null && request.FeatureImageByte.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}.jpg";
                newDoctor.FeatureImagePath = await _imageService.SaveImage(request.FeatureImageByte, fileName, 75);
                _dbContext.Doctors.Update(newDoctor);
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            transaction.Commit();
            result.Success = true;
            result.Data = newDoctor.DoctorId;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }
        finally
        {
            transaction.Dispose();
        }
        return result;
    }
}
