using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentSignupDto : AccountDto
    {
        public string ClassCode { get; set; }
        public string Reference { get; set; }
    }


    public class StudentSignupValidator : AbstractValidator<StudentSignupDto>
    {
        public StudentSignupValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(4);
            RuleFor(x => x.ClassCode).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.Reference).NotEmpty().NotNull().MaximumLength(100);

        }
    }
}
