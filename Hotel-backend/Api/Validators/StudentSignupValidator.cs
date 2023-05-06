using Common.Dto;
using FluentValidation;
using Service;

namespace Api.Validators
{
    public class StudentSignupValidator : AbstractValidator<StudentSignupDto>
    {
        public StudentSignupValidator(IClassSessionService classSessionService)
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(4);
            RuleFor(x => x.Reference).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.ClassCode).NotEmpty().NotNull().MinimumLength(5)
                .Must(x =>
                {
                    var classL = classSessionService.ClassList().FirstOrDefault(classdto => classdto.Code == x);
                    return classL != null;
                }).WithMessage("Class code is invalid");

        }
    }
}
