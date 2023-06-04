using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Month
{
    public int MonthId { get; set; }
    public int ClassId { get; set; }
    public int Sequence { get; set; }
    public decimal TotalMarket { get; set; }
    public bool IsComplete { get; set; }
    public int ConfigId { get; set; }
    public virtual ClassSession Class { get; set; }
    public virtual List<MarketingDecision> MarketingDecision { get; set; }
    public virtual List<PriceDecision> PriceDecision { get; set; }
    public virtual List<AttributeDecision> AttributeDecision { get; set; }


}

public class MonthEntityConfig : IEntityTypeConfiguration<Month>
{
    public void Configure(EntityTypeBuilder<Month> builder)
    {
        builder.ToTable("ClassMonth");
        builder.HasKey(x => x.MonthId);
        builder.Property(x => x.MonthId).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne(x => x.Class).WithMany(x => x.Months).HasForeignKey(x => x.ClassId);
        builder.HasIndex(x => x.ClassId).HasDatabaseName("IX_ClassGroup_ClassID");

        builder.Property(x => x.Sequence).IsRequired();
        builder.Property(x => x.TotalMarket).IsRequired();
        builder.Property(x => x.IsComplete).HasDefaultValue(false);
        builder.Property(x => x.ConfigId);

    }

}