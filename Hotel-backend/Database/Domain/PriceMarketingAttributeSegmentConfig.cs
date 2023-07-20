using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PriceMarketingAttributeSegmentConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string PMA { get; set; }
    public string Segment { get; set; }
    public decimal Percentage { get; set; }

}
public class PriceMarketingAttributeSegmentConfigEntityConfig : IEntityTypeConfiguration<PriceMarketingAttributeSegmentConfig>
{
    void IEntityTypeConfiguration<PriceMarketingAttributeSegmentConfig>.Configure(EntityTypeBuilder<PriceMarketingAttributeSegmentConfig> builder)
    {
        builder.ToTable("PriceMarketingAttributeSegmentConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.PMA).IsRequired();
        builder.Property(x => x.Segment).IsRequired();
        builder.Property(x => x.Percentage).HasDefaultValue(0);

    }
}
