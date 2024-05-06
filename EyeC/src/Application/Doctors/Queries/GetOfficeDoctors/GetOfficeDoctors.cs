using EyeC.Application.Common.Interfaces;

namespace EyeC.Application.Doctors.Queries.GetOfficeDoctors;
public record GetOfficeDoctorsQuery(int OfficeId) : IRequest<IEnumerable<GetOfficeDoctorsViewModel>>
{
}

public class GetOfficeDoctorsQueryHandler: IRequestHandler<GetOfficeDoctorsQuery, IEnumerable<GetOfficeDoctorsViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetOfficeDoctorsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetOfficeDoctorsViewModel>> Handle(GetOfficeDoctorsQuery request, CancellationToken cancellationToken)
    {
        if (request == null || request.OfficeId <= 0) return Enumerable.Empty<GetOfficeDoctorsViewModel>();
        var doctors = await _dbContext.Doctors.Where(i => !i.IsDeleted && i.OfficeId == request.OfficeId)
                                                .ProjectTo<GetOfficeDoctorsViewModel>(_mapper.ConfigurationProvider)
                                                .ToListAsync();

        return doctors;
    }
}
