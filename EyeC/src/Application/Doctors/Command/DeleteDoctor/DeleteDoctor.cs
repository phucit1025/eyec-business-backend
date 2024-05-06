using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;

namespace EyeC.Application.Doctors.Command.DeleteDoctor;
public record DeleteDoctorCommand(int DoctorId) : IRequest
{
}

public class DeleteDoctorCommandHandler: IRequestHandler<DeleteDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public DeleteDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        if (request.DoctorId <= 0) return;
        var doctor = await _dbContext.Doctors.FindAsync(request.DoctorId);
        if(doctor == null) return;
        doctor.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
