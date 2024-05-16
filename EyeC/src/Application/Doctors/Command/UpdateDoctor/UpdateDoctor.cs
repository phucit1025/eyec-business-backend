using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;
using EyeC.Domain.Enums;

namespace EyeC.Application.Doctors.Command.UpdateDoctor;
public record UpdateDoctorCommand : IRequest<ResultModel>
{
    public int DoctorId { get; init; }
    public string? FullName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public Gender Gender { get; init; }
    public string? PersonalExperience { get; init; }
    public string? Education { get; init; }
    public byte[]? FeatureImageByte { get; init; }
}

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    private readonly IImageService _imageService;

    public UpdateDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext, IImageService imageService)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _imageService = imageService;
    }

    public async Task<ResultModel> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();

        try
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(i => i.DoctorId == request.DoctorId && !i.IsDeleted, cancellationToken);
            if (doctor == null)
            {
                result.Success = false;
                result.Error = "Doctor is not existed";
            }
            else
            {
                doctor.FullName = request.FullName!;
                doctor.PhoneNumber = request.PhoneNumber!;
                doctor.Email = request.Email!;
                doctor.Gender = request.Gender;
                doctor.PersonalExperience = request.PersonalExperience!;
                doctor.Education = request.Education!;

                if (request.FeatureImageByte != null && request.FeatureImageByte.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}.jpg";
                    doctor.FeatureImagePath = await _imageService.SaveImage(request.FeatureImageByte, fileName, 75);
                }
                _dbContext.Doctors.Update(doctor);
                await _dbContext.SaveChangesAsync(cancellationToken);
                result.Success = true;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.ToString();
        }
        return result;
    }
}
