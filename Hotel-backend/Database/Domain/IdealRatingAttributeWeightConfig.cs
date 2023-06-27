using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class IdealRatingAttributeWeightConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string Attribute { get; set; }
    public string Segment { get; set; }
    public decimal Weight { get; set; }
    public decimal IdealRating { get; set; }

}
public class IdealRatingAttributeWeightConfigEntityConfig : IEntityTypeConfiguration<IdealRatingAttributeWeightConfig>
{
    public void Configure(EntityTypeBuilder<IdealRatingAttributeWeightConfig> builder)
    {
        builder.ToTable("IdealRatingAttributeWeightConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.Attribute).IsRequired();
        builder.Property(x => x.Segment);
        builder.Property(x => x.Weight);
        builder.Property(x => x.IdealRating);

    }

}

