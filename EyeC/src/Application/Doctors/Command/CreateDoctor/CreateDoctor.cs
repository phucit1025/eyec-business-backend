using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Domain.Enums;

namespace EyeC.Application.Doctors.Command.CreateDoctor;
public record CreateDoctorCommand : IRequest<int>
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

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public CreateDoctorCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
