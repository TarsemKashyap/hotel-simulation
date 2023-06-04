using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Segment
{
    public int ID { get; set; }
    public string SegmentName { get; set; }
    public int Status { get; set; }
    public int MaxRating { get; set; }
}

public class SegmentEntityConfig : IEntityTypeConfiguration<Segment>

{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder.ToTable("Segment");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.SegmentName);
        builder.Property(x => x.Status).HasDefaultValue(1);
        builder.Property(x => x.MaxRating);
    }
}
