using FluentValidation;
public class ClassGroupDto
{
    public int? GroupId { get; set; }
    public int Serial { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public ActionOnRecord Action { get; set; }
}


public enum ActionOnRecord
{
    Updated = 0, Added = 1, Removed = 2
}

public class ClassGroupDtoValidator : AbstractValidator<ClassGroupDto>
{
    public ClassGroupDtoValidator()
    {
        RuleFor(x => x.Serial).NotNull().NotEmpty().GreaterThan(0).WithMessage("Serial should not be 0");
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Balance).NotNull().NotEmpty().GreaterThan(0);

    }
}