using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Authentication.Command.Login;
public class LoginRequest : IRequest<ResultModel>
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}

public class LoginRequestHandler : IRequestHandler<LoginRequest, ResultModel>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IAuthenticationService _authenticationService;

    public LoginRequestHandler(IApplicationDbContext dbContext, IIdentityService identityService, IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _authenticationService = authenticationService;
    }

    public async Task<ResultModel> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();

        try
        {
            var user = await _identityService.GetUserAsync(request.UserName,request.Password);
            if (user == null)
            {
                result.Success = true;
                result.Message = "Email or Password is not existed";
            }
            else
            {
                var token = _authenticationService.BuildAccessToken(user);
                result.Success = true;
                result.Data = token;
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
