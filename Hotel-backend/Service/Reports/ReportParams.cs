﻿namespace Service;

public record ReportParams
{
    public string UserId { get; set; }
    public int ClassId { get; set; }
    public int CurrentQuarter { get; set; }
    public int GroupId { get; set; }
    //todo: add monthId filter to all reports
    public int MonthId { get; set; }
}