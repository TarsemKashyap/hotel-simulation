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
    public decimal LongDebt { get; set; }
    public decimal LongDebtPay { get; set; }
    public decimal ShortDebt { get; set; }
    public decimal ShortDebtPay { get; set; }

    public decimal longBorrow { get; set; }
    public int TotLiab { get; set; }
    public int RetainedEarn { get; set; }
}

