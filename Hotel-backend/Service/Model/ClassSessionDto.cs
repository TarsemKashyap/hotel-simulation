using System;
using FluentValidation;
public class ClassSessionDto
{
    public int ClassId { get; set; }
    public string Title { get; set; }
    public string Memo { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int HotelsCount { get; set; }
    public int RoomInEachHotel { get; set; }
    public int CurrentQuater { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Code { get; set; }

}

public class ClassSessionDtoDtoValidator : AbstractValidator<ClassSessionDto>
{
    public ClassSessionDtoDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Class name should not be empty or null");
        RuleFor(x => x.StartDate).NotNull().NotEmpty().WithMessage("Start date should not be empty or null");
        RuleFor(x => x.EndDate).NotNull().NotEmpty().WithMessage("Start date should not be empty or null");
        RuleFor(x => x.HotelsCount).GreaterThan(0).WithMessage("Hotel count should be greater than 0");
        RuleFor(x => x.RoomInEachHotel).GreaterThan(0).WithMessage("Room in hotel should be 500");
        RuleFor(x => x.CurrentQuater).GreaterThan(0).WithMessage("Room in hotel should be 500");

    }
}