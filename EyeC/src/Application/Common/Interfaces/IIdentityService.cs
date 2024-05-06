using EyeC.Application.Common.Models;
using System.Threading.Tasks;

namespace EyeC.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<List<IdentityUserModel>> GetUsersAsync();
    Task<IdentityUserModel?> GetUserAsync(string email, string password);
}
