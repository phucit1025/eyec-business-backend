using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Application.Authentication.Command.Login;
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(i => i.UserName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(i => i.Password)
            .MaximumLength(250)
            .NotEmpty();
    }
}
