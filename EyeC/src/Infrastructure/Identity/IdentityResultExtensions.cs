using EyeC.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace EyeC.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public static List<IdentityUserModel> ToIdentityUserModels(this List<ApplicationUser>? users)
    {
        if (users == null) return new List<IdentityUserModel>();
        return users.Select(i => new IdentityUserModel
        {
            Id = i.Id,
            UserName = i.NormalizedUserName ?? "",
            Email = i.NormalizedEmail ?? "",
            PhoneNumber = i.PhoneNumber ?? "",
        }).ToList();
    }

    public static IdentityUserModel ToIdentityUserModel(this ApplicationUser user)
    {
        return new IdentityUserModel
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? ""
        };
    }
}
