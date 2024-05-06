using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Application.Doctors.Command.UpdateDoctor;
public class UpdateDoctorCommandValidator: AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(i => i.FullName)
            .NotNull()
            .NotEmpty();

        RuleFor(i => i.Gender)
            .IsInEnum();
    }
}
