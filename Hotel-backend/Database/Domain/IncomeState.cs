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
    public int Room1 { get; set; }
    public int FoodB { get; set; }
    public int FoodB1 { get; set; }
    public int FoodB2 { get; set; }
    public int FoodB3 { get; set; }
    public int FoodB4 { get; set; }
    public int FoodB5 { get; set; }
    public int Other { get; set; }
    public int Other1 { get; set; }
    public int Other2 { get; set; }
    public int Other3 { get; set; }
    public int Other4 { get; set; }
    public int Other5 { get; set; }
    public int Other6 { get; set; }
    public int Rent { get; set; }
    public int TotReven { get; set; }
    public int Room { get; set; }
    public int Food2B { get; set; }
    public int Other7 { get; set; }
    public int TotExpen { get; set; }
    public int TotDeptIncom { get; set; }
    public int UndisExpens1 { get; set; }
    public int UndisExpens2 { get; set; }
    public int UndisExpens3 { get; set; }
    public int UndisExpens4 { get; set; }
    public int UndisExpens5 { get; set; }
    public int UndisExpens6 { get; set; }
    public int GrossProfit { get; set; }
    public int MgtFee { get; set; }
    public int IncomBfCharg { get; set; }
    public int Property { get; set; }
    public int Insurance { get; set; }
    public int Interest { get; set; }
    public int PropDepreciationerty { get; set; }
    public int TotCharg { get; set; }
    public int NetIncomBfTAX { get; set; }
    public int Replace { get; set; }
    public int AjstNetIncom { get; set; }
    public int IncomTAX { get; set; }
    public int NetIncom { get; set; }
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
        builder.Property(x => x.Room1);
        builder.Property(x => x.FoodB);
        builder.Property(x => x.FoodB1);
        builder.Property(x => x.FoodB2);
        builder.Property(x => x.FoodB3);
        builder.Property(x => x.FoodB4);
        builder.Property(x => x.FoodB5);
        builder.Property(x => x.Other);
        builder.Property(x => x.Other1);
        builder.Property(x => x.Other2);
        builder.Property(x => x.Other3);
        builder.Property(x => x.Other4);
        builder.Property(x => x.Other5);
        builder.Property(x => x.Other6);
        builder.Property(x => x.Rent);
        builder.Property(x => x.TotReven);
        builder.Property(x => x.Room);
        builder.Property(x => x.Food2B);
        builder.Property(x => x.Other7);
        builder.Property(x => x.TotExpen);
        builder.Property(x => x.TotDeptIncom);
        builder.Property(x => x.UndisExpens1);
        builder.Property(x => x.UndisExpens2);
        builder.Property(x => x.UndisExpens3);
        builder.Property(x => x.UndisExpens4);
        builder.Property(x => x.UndisExpens5);
        builder.Property(x => x.UndisExpens6);
        builder.Property(x => x.GrossProfit);
        builder.Property(x => x.MgtFee);
        builder.Property(x => x.IncomBfCharg);
        builder.Property(x => x.Property);
        builder.Property(x => x.Insurance);
        builder.Property(x => x.Interest);
        builder.Property(x => x.PropDepreciationerty);
        builder.Property(x => x.TotCharg);
        builder.Property(x => x.NetIncomBfTAX);
        builder.Property(x => x.Replace);
        builder.Property(x => x.AjstNetIncom);
        builder.Property(x => x.IncomTAX);
        builder.Property(x => x.NetIncom);
    }
}

