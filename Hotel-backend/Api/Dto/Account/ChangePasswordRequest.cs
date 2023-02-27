using FluentValidation;

namespace Api.Dto;
public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword).NotNull().NotEmpty();
        RuleFor(x => x.NewPassword).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.CurrentPassword).NotEqual(x => x.NewPassword).WithMessage("Old and new password count not be same.");

    }
}
