using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Domain.Enums;

namespace EyeC.Application.Doctors.Command.UpdateDoctor;
public record UpdateDoctorCommand : IRequest
{
    public string? FullName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public Gender Gender { get; init; }
    public string? PersonalExperience { get; init; }
    public int? OfficeId { get; init; }
    public string? Education { get; init; }
    public byte[]? FeatureImageByte { get; init; }
}

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public UpdateDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public Task Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
