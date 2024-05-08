using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;

namespace EyeC.Application.Offices.Queries.GetOffices;
public record GetOfficesQuery: IRequest<IEnumerable<OfficeViewModel>>
{
}

public class GetOfficesQueryHanlder: IRequestHandler<GetOfficesQuery, IEnumerable<OfficeViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetOfficesQueryHanlder(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OfficeViewModel>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Offices.Where(i => !i.IsDeleted)
                                        .ProjectTo<OfficeViewModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
    }
}
