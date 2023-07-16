using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BalanceSheetDto
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
}

