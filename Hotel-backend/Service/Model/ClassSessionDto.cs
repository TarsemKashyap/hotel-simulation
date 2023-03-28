using System;
using FluentValidation;

public class ClassSessionDto
{
    public int ClassId { get; set; }
    public string Title { get; set; }
    public string Memo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int HotelsCount { get; set; }
    public int RoomInEachHotel { get; set; }
    public int CurrentQuater { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Code { get; set; }

}

public class ClassSessionDtoValidator : AbstractValidator<ClassSessionDto>
{
    public ClassSessionDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Class name should not be empty or null");
        RuleFor(x => x.StartDate).NotNull().NotEmpty()
        .WithMessage("Start date should not be empty or null");
        //.Must(x => ValidateData(x)).WithMessage("Start date is not valid");
        RuleFor(x => x.EndDate).NotNull().NotEmpty().WithMessage("Start date should not be empty or null");
        //.Must(x => ValidateData(x)).WithMessage("End date is not valid");
        RuleFor(x => x.HotelsCount).GreaterThan(0).WithMessage("Hotel count should be greater than 0");
        RuleFor(x => x.RoomInEachHotel).GreaterThan(0).WithMessage("Room in hotel should be 500");
        RuleFor(x => x.CurrentQuater).GreaterThanOrEqualTo(0).WithMessage("Current quarter should not be null");

    }

    private bool ValidateData(string value)
    {
        return DateTime.TryParse(value, out DateTime result);
    }
}