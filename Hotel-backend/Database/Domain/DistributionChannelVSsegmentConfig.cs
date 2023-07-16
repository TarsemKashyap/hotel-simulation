using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DistributionChannelVSsegmentConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string Segment { get; set; }
    public string DistributionChannel { get; set; }
    public decimal Percentage { get; set; }
    public decimal CostPercent { get; set; }



}
public class DistributionChannelVSsegmentConfigEntityConfig : IEntityTypeConfiguration<DistributionChannelVSsegmentConfig>
{
    public void Configure(EntityTypeBuilder<DistributionChannelVSsegmentConfig> builder)
    {
        builder.ToTable("DistributionChannelVSsegmentConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.Segment).IsRequired();
        builder.Property(x => x.DistributionChannel).IsRequired();
        builder.Property(x => x.Percentage).HasDefaultValue(0);
        builder.Property(x => x.CostPercent).HasDefaultValue(0);

    }

}
