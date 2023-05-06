using Common.Dto;
using FluentValidation;
using Service;

namespace Api.Validators
{
    public class StudentSignupTempDtoValidator : AbstractValidator<StudentSignupTempDto>
    {
        public StudentSignupTempDtoValidator(IClassSessionService classSessionService)
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName name should not be empty or null");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("FirstName name should not be empty or null");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email name should not be empty or null");
            RuleFor(x => x.Institute).NotNull().NotEmpty().WithMessage("Institute name should not be empty or null");
            RuleFor(x => x.ClassCode).NotNull().NotEmpty().WithMessage("Class Code should not be empty or null")
                .Must(x =>
                {
                    var classL = classSessionService.ClassList().FirstOrDefault(classdto => classdto.Code == x);
                    return classL != null;
                }).WithMessage("Class code is invalid");


        }

        private bool ValidateData(string value)
        {
            return DateTime.TryParse(value, out DateTime result);
        }
    }
}
