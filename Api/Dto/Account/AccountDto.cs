using FluentValidation;
public class AccountDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class AccountDtoValidator : AbstractValidator<AccountDto>
{
    public AccountDtoValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x=>x.LastName).NotNull().NotEmpty();
        RuleFor(x=>x.UserName).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x=>x.Password).NotEmpty().NotNull().MinimumLength(4);
        
    }
}