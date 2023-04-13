using FluentValidation;


public class InstructorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string institute { get; set; }
}
public class InstructorAccountRequest : InstructorDto
{
    public string Password { get; set; }
}

public class InstructorAccountDtoValidator : AbstractValidator<InstructorAccountRequest>
{
    public InstructorAccountDtoValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(4);

    }
}