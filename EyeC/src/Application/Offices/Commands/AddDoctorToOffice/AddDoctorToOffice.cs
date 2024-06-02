using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Offices.Commands.AddDoctorToOffice;
public record AddDoctorToOfficeCommand(int DoctorId, int OfficeId) : IRequest<ResultModel>
{
}

public class AddDoctorToOfficeHandler : IRequestHandler<AddDoctorToOfficeCommand, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public AddDoctorToOfficeHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ResultModel> Handle(AddDoctorToOfficeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        try
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(i => i.Id == request.DoctorId && !i.IsDeleted, cancellationToken);

            if (doctor == null)
            {
                result.Error = "Doctor is not existed";
                result.Success = false;
            }
            else
            {
                doctor.OfficeId = request.OfficeId;
                
                _dbContext.Doctors.Update(doctor);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                result.Success = true;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }
        return result;
    }
}
