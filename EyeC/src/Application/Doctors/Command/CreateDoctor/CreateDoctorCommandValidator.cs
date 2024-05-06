using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Application.Doctors.Command.CreateDoctor;
public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(i => i.FullName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(i => i.Gender)
            .IsInEnum();
    }
}
