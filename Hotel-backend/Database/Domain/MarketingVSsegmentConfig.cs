using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MarketingVSsegmentConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string Segment { get; set; }
    public string MarketingTechniques { get; set; }
    public double Percentage { get; set; }
    public double LaborPercent { get; set; }


}
public class MarketingVSsegmentConfigConfig : IEntityTypeConfiguration<MarketingVSsegmentConfig>
{
    public void Configure(EntityTypeBuilder<MarketingVSsegmentConfig> builder)
    {
        builder.ToTable("MarketingVSsegmentConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.Segment).IsRequired();
        builder.Property(x => x.Percentage).HasDefaultValue(0);
        builder.Property(x => x.MarketingTechniques).IsRequired();
        builder.Property(x => x.LaborPercent).IsRequired();

    }

}

