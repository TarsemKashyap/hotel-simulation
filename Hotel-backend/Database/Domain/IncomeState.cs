using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class IncomeState
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public decimal Room1 { get; set; }
    public decimal FoodB { get; set; }
    public decimal FoodB1 { get; set; }
    public decimal FoodB2 { get; set; }
    public decimal FoodB3 { get; set; }
    public decimal FoodB4 { get; set; }
    public decimal FoodB5 { get; set; }
    public decimal Other { get; set; }
    public decimal Other1 { get; set; }
    public decimal Other2 { get; set; }
    public decimal Other3 { get; set; }
    public decimal Other4 { get; set; }
    public decimal Other5 { get; set; }
    public decimal Other6 { get; set; }
    public decimal Rent { get; set; }
    public decimal TotReven { get; set; }
    public decimal Room { get; set; }
    public decimal Food2B { get; set; }
    public decimal Other7 { get; set; }
    public decimal TotExpen { get; set; }
    public decimal TotDeptIncom { get; set; }
    public decimal UndisExpens1 { get; set; }
    public decimal UndisExpens2 { get; set; }
    public decimal UndisExpens3 { get; set; }
    public decimal UndisExpens4 { get; set; }
    public decimal UndisExpens5 { get; set; }
    public decimal UndisExpens6 { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal MgtFee { get; set; }
    public decimal IncomBfCharg { get; set; }
    public decimal Property { get; set; }
    public decimal Insurance { get; set; }
    public decimal Interest { get; set; }
    public decimal PropDepreciationerty { get; set; }
    public decimal TotCharg { get; set; }
    public decimal NetIncomBfTAX { get; set; }
    public decimal Replace { get; set; }
    public decimal AjstNetIncom { get; set; }
    public decimal IncomTAX { get; set; }
    public decimal NetIncom { get; set; }
    public virtual Month Month { get; set; }

}
public class IncomeStateEntityConfig : IEntityTypeConfiguration<IncomeState>
{
    public void Configure(EntityTypeBuilder<IncomeState> builder)
    {
        builder.ToTable("IncomeState");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.IncomeState);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Room1).HasPrecision(19, 4);
        builder.Property(x => x.FoodB).HasPrecision(19, 4);
        builder.Property(x => x.FoodB1).HasPrecision(19, 4);
        builder.Property(x => x.FoodB2).HasPrecision(19, 4);
        builder.Property(x => x.FoodB3).HasPrecision(19, 4);
        builder.Property(x => x.FoodB4).HasPrecision(19, 4);
        builder.Property(x => x.FoodB5).HasPrecision(19, 4);
        builder.Property(x => x.Other).HasPrecision(19, 4);
        builder.Property(x => x.Other1).HasPrecision(19, 4);
        builder.Property(x => x.Other2).HasPrecision(19, 4);
        builder.Property(x => x.Other3).HasPrecision(19, 4);
        builder.Property(x => x.Other4).HasPrecision(19, 4);
        builder.Property(x => x.Other5).HasPrecision(19, 4);
        builder.Property(x => x.Other6).HasPrecision(19, 4);
        builder.Property(x => x.Rent).HasPrecision(19, 4);
        builder.Property(x => x.TotReven).HasPrecision(19, 4);
        builder.Property(x => x.Room).HasPrecision(19, 4);
        builder.Property(x => x.Food2B).HasPrecision(19, 4);
        builder.Property(x => x.Other7).HasPrecision(19, 4);
        builder.Property(x => x.TotExpen).HasPrecision(19, 4);
        builder.Property(x => x.TotDeptIncom).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens1).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens2).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens3).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens4).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens5).HasPrecision(19, 4);
        builder.Property(x => x.UndisExpens6).HasPrecision(19, 4);
        builder.Property(x => x.GrossProfit).HasPrecision(19, 4);
        builder.Property(x => x.MgtFee).HasPrecision(19, 4);
        builder.Property(x => x.IncomBfCharg).HasPrecision(19, 4);
        builder.Property(x => x.Property).HasPrecision(19, 4);
        builder.Property(x => x.Insurance).HasPrecision(19, 4);
        builder.Property(x => x.Interest).HasPrecision(19, 4);
        builder.Property(x => x.PropDepreciationerty).HasPrecision(19, 4);
        builder.Property(x => x.TotCharg).HasPrecision(19, 4);
        builder.Property(x => x.NetIncomBfTAX).HasPrecision(19, 4);
        builder.Property(x => x.Replace).HasPrecision(19, 4);
        builder.Property(x => x.AjstNetIncom).HasPrecision(19, 4);
        builder.Property(x => x.IncomTAX).HasPrecision(19, 4);
        builder.Property(x => x.NetIncom).HasPrecision(19, 4);
    }
}

