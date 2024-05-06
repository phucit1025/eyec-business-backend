using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Common.Interfaces;
public interface IAuthenticationService
{
    string BuildAccessToken(IdentityUserModel user);
}
