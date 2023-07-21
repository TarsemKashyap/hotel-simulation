namespace Service;

public record ReportParams
{
    public string UserId { get; set; }
    public int ClassId { get; set; }
    public int CurrentQuarter { get; set; }
    public int GroupId { get; set; }
}