using FluentValidation;


public class ChangePasswordRequest
{
    public string ConfirmPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEqual(x => x.NewPassword).WithMessage("Old and new password count not be same.");
        RuleFor(x => x.NewPassword).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);

    }
}
