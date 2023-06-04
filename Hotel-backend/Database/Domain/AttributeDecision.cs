using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AttributeDecision
{
    public int ID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Attribute { get; set; }
    public int AccumulatedCapital { get; set; }
    public int NewCapital { get; set; }
    public int OperationBudget { get; set; }
    public int LaborBudget { get; set; }
    public bool Confirmed { get; set; }
    public int QuarterForecast { get; set; }
    public int MonthID { get; set; }
    public virtual Month Month { get; set; }
}
public class AttributeDecisionEntityConfig : IEntityTypeConfiguration<AttributeDecision>

{
    public void Configure(EntityTypeBuilder<AttributeDecision> builder)
    {
        builder.ToTable("AttributeDecision");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.AttributeDecision);
        builder.Property(x => x.QuarterNo);
        builder.Property(x => x.GroupID);
        builder.Property(x => x.Attribute);
        builder.Property(x => x.AccumulatedCapital);
        builder.Property(x => x.NewCapital);
        builder.Property(x => x.OperationBudget);
        builder.Property(x => x.LaborBudget);
        builder.Property(x => x.Confirmed).HasDefaultValue(false);
        builder.Property(x => x.QuarterForecast);
    }
}

