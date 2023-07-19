namespace Service;

public record GoalReportParams
{
    public string UserId { get; set; }
    public int ClassId { get; set; }
    public int CurrentQuarter { get; set; }
    public int GroupId { get; set; }
}