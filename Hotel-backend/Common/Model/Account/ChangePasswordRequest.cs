using FluentValidation;


public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.NewPassword).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.ConfirmPassword).NotEqual(x => x.NewPassword).WithMessage("Old and new password count not be same.");

    }
}
