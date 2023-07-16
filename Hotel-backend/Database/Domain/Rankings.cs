using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Rankings
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int Month { get; set; }
    public int TeamNo { get; set; }
    public string TeamName { get; set; }
    public string Institution { get; set; }
    public string Indicator { get; set; }
    public decimal Performance { get; set; }
    public DateTime Time { get; set; }
}

public class RankingsEntityConfig : IEntityTypeConfiguration<Rankings>
{
    public void Configure(EntityTypeBuilder<Rankings> builder)
    {
        builder.ToTable("Rankings");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.Property(x => x.Month).IsRequired();
        builder.Property(x => x.TeamNo).IsRequired();
        builder.Property(x => x.TeamName);
        builder.Property(x => x.Institution);
        builder.Property(x => x.Indicator);
        builder.Property(x => x.Time);
        
    }
}