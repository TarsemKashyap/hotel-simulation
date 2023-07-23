using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeekdayVSsegmentConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public bool WeekDay { get; set; }
    public string Segment { get; set; }
    public decimal Percentage { get; set; }
    public decimal PriceExpectation { get; set; }

}
public class WeekdayVSsegmentConfigEntityConfig : IEntityTypeConfiguration<WeekdayVSsegmentConfig>
{
    public void Configure(EntityTypeBuilder<WeekdayVSsegmentConfig> builder)
    {
        builder.ToTable("WeekdayVSsegmentConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.WeekDay).IsRequired();
        builder.Property(x => x.Percentage).IsRequired();
        builder.Property(x => x.PriceExpectation).IsRequired();
        builder.Property(x => x.Segment);

    }
}

