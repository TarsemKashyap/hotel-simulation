using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SegmentConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string Segment { get; set; }
    public double Percentage { get; set; }

}
public class SegmentConfigEntityConfig : IEntityTypeConfiguration<SegmentConfig>
{
    public void Configure(EntityTypeBuilder<SegmentConfig> builder)
    {
        builder.ToTable("SegmentConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.Segment).IsRequired();
        builder.Property(x => x.Percentage).HasDefaultValue(0);

    }

}
