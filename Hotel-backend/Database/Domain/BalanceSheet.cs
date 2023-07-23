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
    public decimal Cash { get; set; }
    public decimal AcctReceivable { get; set; }
    public decimal Inventories { get; set; }
    public decimal TotCurrentAsset { get; set; }
    public decimal NetPrptyEquip { get; set; }
    public decimal TotAsset { get; set; }
    public decimal TotCurrentLiab { get; set; }
    public decimal LongDebt { get; set; }
    public decimal LongDebtPay { get; set; }
    public decimal ShortDebt { get; set; }
    public decimal ShortDebtPay { get; set; }
    public decimal TotLiab { get; set; }
    public decimal RetainedEarn { get; set; }
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


