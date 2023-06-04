using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class BalanceSheet
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public int Cash { get; set; }
    public int AcctReceivable { get; set; }
    public int Inventories { get; set; }
    public int TotCurrentAsset { get; set; }
    public int NetPrptyEquip { get; set; }
    public int TotAsset { get; set; }
    public int TotCurrentLiab { get; set; }
    public int LongDebt { get; set; }
    public int LongDebtPay { get; set; }
    public int ShortDebt { get; set; }
    public int ShortDebtPay { get; set; }
    public int TotLiab { get; set; }
    public int RetainedEarn { get; set; }
    public virtual Month Month { get; set; }

}
public class BalanceSheetConfig : IEntityTypeConfiguration<BalanceSheet>
{
    public void Configure(EntityTypeBuilder<BalanceSheet> builder)
    {
        builder.ToTable("BalanceSheet");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.BalanceSheet);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Cash);
        builder.Property(x => x.AcctReceivable);
        builder.Property(x => x.Inventories);
        builder.Property(x => x.TotCurrentAsset);
        builder.Property(x => x.NetPrptyEquip);
        builder.Property(x => x.TotAsset);
        builder.Property(x => x.TotCurrentLiab);
        builder.Property(x => x.LongDebt);
        builder.Property(x => x.LongDebtPay);
        builder.Property(x => x.ShortDebt);
        builder.Property(x => x.ShortDebtPay);
        builder.Property(x => x.TotLiab);
        builder.Property(x => x.RetainedEarn);
    }
}


