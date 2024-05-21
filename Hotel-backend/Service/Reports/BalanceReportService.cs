using Common.ReportDto;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Mysqlx.Crud;

namespace Service;

public interface IBalanceReportService
{
    Task<BalanceReportDto> Report(ReportParams reportParams);
}

public class BalanceReportService : AbstractReportService, IBalanceReportService
{
    private readonly HotelDbContext _context;

    public BalanceReportService(HotelDbContext context)
    {
        _context = context;
    }
    public async Task<BalanceReportDto> Report(ReportParams reportParams)
    {
        int quater = reportParams.CurrentQuarter, groupId = reportParams.GroupId, monthId = reportParams.MonthId;
        BalanceReportDto report = new BalanceReportDto();
        BalanceSheet balance = await _context.BalanceSheet.AsNoTracking().FirstOrDefaultAsync(x => x.MonthID == monthId && x.QuarterNo == quater && x.GroupID == groupId);

        CurrentAssests currentAssests = new CurrentAssests();
        currentAssests.Cash.Data = Money(balance.Cash);
        currentAssests.AccountReceivables.Data = Money(balance.AcctReceivable);

        currentAssests.AccountReceivables.AddChild("Less Allowance for Doubtful Accounts", Money((balance.AcctReceivable * 3) / 100));
        currentAssests.AccountReceivables.AddChild("Net Receivables", Money((balance.AcctReceivable * 97) / 100));
        currentAssests.Inventories.Data = Money(balance.Inventories);
        currentAssests.Total.Data = Money(balance.TotCurrentAsset);

        report.CurrentAssests = currentAssests;


        decimal accumuNewInvest = 0;
        if (quater > 1)
        {
            accumuNewInvest = await _context.AttributeDecision.AsNoTracking().Where(x => x.QuarterNo < quater && x.GroupID == groupId).SumAsync(x => x.NewCapital);
        }
        //PROPERT AND EQUIPMENT
        decimal propEquip = 69813710 + accumuNewInvest;
        PropertyEquipement propertyEquipement = new PropertyEquipement();
        propertyEquipement.PropertyAndEquipment.Data = Money(propEquip);
        propertyEquipement.LessAccumlatedDepreciation.Data = Money(propEquip - balance.NetPrptyEquip);
        propertyEquipement.NetPropertyAndEquipment.Data = Money(balance.NetPrptyEquip);
        report.PropertyEquipement = propertyEquipement;

        // Other Assests
        report.OtherAssets.AddChild(Money("Intangible Assests", 5000000));
        report.OtherAssets.AddChild(Money("Total Other Assets", 5000000));

        // total Assests
        report.TotalAssests.Data = Money(balance.TotAsset);

        // LIABILITIES AND OWNER'S EQUITY
        ReportAttribute currentLibailities = "Current Liabilities";

        //      LongTerm debt
        ReportAttribute longTermDebt = Money("Long Term debt", balance.LongDebt);

        //short errm
        ReportAttribute emergencyLoan = Money("Emergency Loan", balance.ShortDebt);
        ReportAttribute totalLibilites = Money("Total Libilities", balance.TotLiab);

        report.RetainedEarnings.Data = Money(balance.RetainedEarn);
        decimal totalShareholder = balance.RetainedEarn + 10000000;
        report.TotalShareHoldersEquity.Data = Money(totalShareholder);
        report.ShareHolderEquity.Data = Money(100000);

        if (quater == 0)
        {
            propertyEquipement.LessAccumlatedDepreciation.Data = Money(24478710);
            propertyEquipement.NetPropertyAndEquipment.Data = Money(45335000);
            decimal accountPayable = 15000, advanceDepostit = 367890, incometaxpayable = 378120;
            currentLibailities.AddChild("Accounts Payable", accountPayable);
            currentLibailities.AddChild("Advance Deposits", advanceDepostit);
            currentLibailities.AddChild("Income Tax Payable", incometaxpayable);
            currentLibailities.AddChild("Total Current Liabilities", accountPayable + advanceDepostit + incometaxpayable);
            decimal totalandtotal = totalShareholder + balance.TotLiab;
            report.TotalLiablitiesAndEquity.Data = Money(totalandtotal);
        }
        else
        {
            decimal totalOperationBudget = await _context.AttributeDecision
                                            .AsNoTracking()
                                            .Where(x => x.QuarterNo == quater && x.GroupID == groupId && x.MonthID == monthId)
                                            .SumAsync(x => x.OperationBudget);
            totalOperationBudget = totalOperationBudget / 5;
            currentLibailities.AddChild("Accounts Payable", totalOperationBudget);

            decimal advanceDeposit = await _context.SoldRoomByChannel.AsNoTracking().Where(x => x.MonthID == monthId && x.QuarterNo == quater && x.GroupID == groupId).SumAsync(x => x.Revenue);
            advanceDeposit = advanceDeposit / 20;

            currentLibailities.AddChild("Advance Deposits", advanceDeposit);

            IncomeState incomeState = await _context.IncomeState.AsNoTracking().Where(x => x.MonthID == monthId && x.GroupID == groupId && x.QuarterNo == quater).FirstOrDefaultAsync();
            currentLibailities.AddChild("Income Tax Payable", incomeState.IncomTAX);
            decimal totCurrentLiab = totalOperationBudget + advanceDeposit + incomeState.IncomTAX;

            currentLibailities.AddChild("Total Current Liabilities", totCurrentLiab);
            decimal totalLib = totCurrentLiab + balance.LongDebt + balance.ShortDebt;
            decimal totalandtotal = totalShareholder + totalLib;
            report.TotalLiablitiesAndEquity.Data = Money(totalandtotal);

        }
        report.LibilitiesAndOwnerEquity = new LibilitiesAndOwnerEquity();
        report.LibilitiesAndOwnerEquity.CurrentLibalities = currentLibailities;
        report.LibilitiesAndOwnerEquity.LongTermDebt = longTermDebt;
        report.LibilitiesAndOwnerEquity.EmergencyLoan = emergencyLoan;
        report.LibilitiesAndOwnerEquity.TotalLibbalities = totalLibilites;
        report.TotalAssests.AddChild(currentLibailities);
        return report;

    }
}





