using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class WeightedAttributeRating
{
    public int ID { get; set; }
    public int MonthID { get; set; }

    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Segment { get; set; }
    public decimal CustomerRating { get; set; }
    public int ActualDemand { get; set; }
    public virtual Month Month { get; set; }
}
public class WeightedAttributeRatingEntityConfig : IEntityTypeConfiguration<WeightedAttributeRating>
{
    public void Configure(EntityTypeBuilder<WeightedAttributeRating> builder)
    {
        builder.ToTable("WeightedAttributeRating");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.WeightedAttributeRating);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Segment);
        builder.Property(x => x.CustomerRating);
        builder.Property(x => x.ActualDemand);
    }
}

