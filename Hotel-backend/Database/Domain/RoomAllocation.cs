using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomAllocation
{
    public int ID { get; set; }
    public int MonthID { get; set; }

    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public bool Weekday { get; set; }
    public string Segment { get; set; }
    public int RoomsAllocated { get; set; }
    public int ActualDemand { get; set; }
    public int RoomsSold { get; set; }
    public bool Confirmed { get; set; }
    public decimal Revenue { get; set; }
    public int QuarterForecast { get; set; }
    public virtual Month Month { get; set; }

}
public class RoomAllocationEntityConfig : IEntityTypeConfiguration<RoomAllocation>
{
    void IEntityTypeConfiguration<RoomAllocation>.Configure(EntityTypeBuilder<RoomAllocation> builder)
    {
        builder.ToTable("RoomAllocation");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.RoomAllocation);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Weekday);
        builder.Property(x => x.Segment);
        builder.Property(x => x.RoomsAllocated);
        builder.Property(x => x.ActualDemand);
        builder.Property(x => x.Confirmed).HasDefaultValue(false);
        builder.Property(x => x.Revenue);
        builder.Property(x => x.QuarterForecast);
    }
}

