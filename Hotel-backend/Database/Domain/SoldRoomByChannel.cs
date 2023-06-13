using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SoldRoomByChannel
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Segment { get; set; }
    public string Channel { get; set; }
    public bool Weekday { get; set; }
    public int Revenue { get; set; }
    public int SoldRoom { get; set; }
    public int Cost { get; set; }
    public virtual Month Month { get; set; }
}
public class SoldRoomByChannelConfig : IEntityTypeConfiguration<SoldRoomByChannel>
{
    public void Configure(EntityTypeBuilder<SoldRoomByChannel> builder)
    {
        builder.ToTable("SoldRoomByChannel");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.SoldRoomByChannel);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Segment);
        builder.Property(x => x.Channel);
        builder.Property(x => x.Weekday);
        builder.Property(x => x.Revenue);
        builder.Property(x => x.SoldRoom);
        builder.Property(x => x.Cost);
    }
}

