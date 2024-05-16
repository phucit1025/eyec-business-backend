using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;
using EyeC.Application.Common.Security;
using EyeC.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Application.Users.Queries.GetUsers;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Roles = Roles.Member)]
public record GetUsersQuery : IRequest<ResultModel>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResultModel>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IApplicationDbContext dbContext, IIdentityService identityService, IMapper mapper)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<ResultModel> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        var users = await _identityService.GetUsersAsync();
        var userModels = _mapper.Map<List<IdentityUserModel>,List<UserViewModel>>(users);
        result.Success = true;
        result.Data = userModels;
        return result;
    }
}
