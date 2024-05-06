using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;
using EyeC.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;

namespace EyeC.Infrastructure.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;

    public AuthenticationService(ApplicationDbContext context, string jwtSecret, string jwtIssuer)
    {
        _context = context;
        _jwtSecret = jwtSecret;
        _jwtIssuer = jwtIssuer;
    }

    public string BuildAccessToken(IdentityUserModel user)
    {
        if (string.IsNullOrEmpty(_jwtSecret) || string.IsNullOrEmpty(_jwtIssuer))
        {
            throw new Exception($"Authentication configuration is missing");
        }
        List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim("Username",user.UserName),
                new Claim("Role", user.Role)
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_jwtIssuer,
          _jwtIssuer,
          claims,
          expires: DateTime.Now.AddDays(90),
          signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
