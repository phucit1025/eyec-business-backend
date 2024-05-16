using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Doctors.Command.DeleteDoctor;
public record DeleteDoctorCommand(int DoctorId) : IRequest<ResultModel>
{
}

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public DeleteDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ResultModel> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        try
        {
            if (request.DoctorId <= 0)
            {
                result.Success = false;
            }
            else
            {
                var doctor = await _dbContext.Doctors.FindAsync(request.DoctorId);
                if (doctor != null)
                {
                    doctor.IsDeleted = true;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
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
