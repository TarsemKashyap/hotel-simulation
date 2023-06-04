using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MarketingDecision
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string MarketingTechniques { get; set; }
    public string Segment { get; set; }
    public int Spending { get; set; }
    public int LaborSpending { get; set; }
    public int ActualDemand { get; set; }
    public bool Confirmed { get; set; }
    public virtual Month Month { get; set; }

}
public class MarketingDecisionEntityConfig : IEntityTypeConfiguration<MarketingDecision>
{
    public void Configure(EntityTypeBuilder<MarketingDecision> builder)
    {
        builder.ToTable("MarketingDecision");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.MarketingDecision);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.MarketingTechniques).IsRequired();
        builder.Property(x => x.Segment);
        builder.Property(x => x.Spending);
        builder.Property(x => x.LaborSpending);
        builder.Property(x => x.ActualDemand);
        builder.Property(x => x.Confirmed).HasDefaultValue(false);
    }
}

