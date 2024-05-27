using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class PriceDecision
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public bool Weekday { get; set; }
    public string DistributionChannel { get; set; }
    public string Segment { get; set; }
    public decimal Price { get; set; }
    public decimal ActualDemand { get; set; }
    public bool Confirmed { get; set; }
    public virtual Month Month { get; set; }

}
public class PriceDecisionEntityConfig : IEntityTypeConfiguration<PriceDecision>
{
    public void Configure(EntityTypeBuilder<PriceDecision> builder)
    {
        builder.ToTable("PriceDecision");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property<int>(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.PriceDecision);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Weekday);
        builder.Property(x => x.DistributionChannel);
        builder.Property(x => x.Price);
        builder.Property(x => x.Segment);
        builder.Property(x => x.ActualDemand);
        builder.Property(x => x.Confirmed).HasDefaultValue(false);
    }
}

