using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;

namespace EyeC.Application.Doctors.Queries.GetAllDoctors;
public record GetAllDoctorsQuery : IRequest<IEnumerable<DoctorViewModel>>
{
}

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<DoctorViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllDoctorsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DoctorViewModel>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _dbContext.Doctors.Where(i => !i.IsDeleted)
                                        .ProjectTo<DoctorViewModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
        return doctors;
    }
}
