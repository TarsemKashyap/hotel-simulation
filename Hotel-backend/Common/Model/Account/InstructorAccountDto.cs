using FluentValidation;


public class InstructorDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Institute { get; set; }
}
public class InstructorSignupRequest : InstructorDto
{
    public string Password { get; set; }
}

public class InstructorSignupValidator : AbstractValidator<InstructorSignupRequest>
{
    public InstructorSignupValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x=>x.Institute).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(4);

    }
}


public class InstructorDtoValidator : AbstractValidator<InstructorDto>
{
    public InstructorDtoValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x=>x.Institute).NotEmpty();
    }
}