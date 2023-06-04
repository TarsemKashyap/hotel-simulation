using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Goal
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public int OccupancyM { get; set; }
    public int OccupancyY { get; set; }
    public int RoomRevenM { get; set; }
    public int RoomRevenY { get; set; }
    public int TotalRevenM { get; set; }
    public int TotalRevenY { get; set; }
    public int ShareRoomM { get; set; }
    public int ShareRoomY { get; set; }
    public int ShareRevenM { get; set; }
    public int ShareRevenY { get; set; }
    public int RevparM { get; set; }
    public int RevparY { get; set; }
    public int ADRM { get; set; }
    public int ADRY { get; set; }
    public int YieldMgtM { get; set; }
    public int YieldMgtY { get; set; }
    public int MgtEfficiencyM { get; set; }
    public int MgtEfficiencyY { get; set; }
    public int ProfitMarginM { get; set; }
    public int ProfitMarginY { get; set; }
    public virtual Month Month { get; set; }

}
public class GoalEntityConfig : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("Goal");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.Goal);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.OccupancyM);
        builder.Property(x => x.OccupancyY);
        builder.Property(x => x.RoomRevenM);
        builder.Property(x => x.RoomRevenY);
        builder.Property(x => x.TotalRevenM);
        builder.Property(x => x.TotalRevenY);
        builder.Property(x => x.ShareRoomM);
        builder.Property(x => x.ShareRoomY);
        builder.Property(x => x.ShareRevenM);
        builder.Property(x => x.ShareRevenY);
        builder.Property(x => x.RevparM);
        builder.Property(x => x.RevparY);
        builder.Property(x => x.ADRM);
        builder.Property(x => x.ADRY);
        builder.Property(x => x.YieldMgtM);
        builder.Property(x => x.YieldMgtY);
        builder.Property(x => x.MgtEfficiencyM);
        builder.Property(x => x.MgtEfficiencyY);
        builder.Property(x => x.ProfitMarginM);
        builder.Property(x => x.ProfitMarginY);
    }
}

