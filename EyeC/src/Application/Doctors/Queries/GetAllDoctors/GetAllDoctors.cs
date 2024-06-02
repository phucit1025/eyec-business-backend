using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Doctors.Queries.GetAllDoctors;
public record GetAllDoctorsQuery : IRequest<ResultModel>
{
}

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllDoctorsQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ResultModel> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        try
        {
            var doctors = await _dbContext.Doctors.Where(i => !i.IsDeleted)
                                        .ProjectTo<DoctorViewModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            result.Success = true;
            result.Data = doctors;
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
        }
        return result;
    }
}
