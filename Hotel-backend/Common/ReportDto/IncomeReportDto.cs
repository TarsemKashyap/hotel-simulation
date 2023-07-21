namespace Common.ReportDto
{
    public class IncomeReportDto
    {
        public Revenue Revenue { get; set; }
        public DepartmentalExpenses DepartmentalExpenses { get; set; }
        public ReportAttribute TotalDepartIncome => new ReportAttribute { Label = "TOTAL DEPARTMENTAL INCOME" };
        public UndistOperatingExpenses UndistOperatingExpenses { get; set; }
        public ReportAttribute GrossOperatingProfit => new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute ManagmentFees => new ReportAttribute { Label = "GROSS OPERATING PROFIT" };
        public ReportAttribute IncomeBeforeFixedCharges => new ReportAttribute { Label = "INCOME BEFORE FIXED CHARGES" };
        public FixedCharges FixedCharges { get; set; }
        public ReportAttribute NetOperatingIncome => new ReportAttribute { Label = "NET OPERATING INCOME (BEFORE TAX)" };
        public ReportAttribute IncomeTax => new ReportAttribute { Label = "Income Tax" };
        public ReportAttribute NetIncome => new ReportAttribute { Label = "NET INCOME" };



    }

    public class Revenue
    {
        public ReportAttribute Rooms { get; set; }
        public FoodBeverage FoodBeverage { get; set; }
        public OtherOperatedDocs OtherOperatedDocs { get; set; }
        public ReportAttribute RentelOtherIncome { get; set; }
        public ReportAttribute TotalRevenue { get; set; }


    }
    public class ReportAttribute
    {
        public string Label { get; set; }
        public Currency Value { get; set; }
        public Dictionary<string, Currency> Currency { get; set; } = new Dictionary<string, Currency>();
        protected void AddKey(string label, decimal? money)
        {
            Currency.Add(label, money.HasValue ? new Currency(money.Value) : null);
        }

        public decimal Sum => Currency.Values.Sum(x => x.Value);
    }

    public class DepartmentalExpenses : ReportAttribute
    {
        public DepartmentalExpenses()
        {
            this.Label = "DEPARTMENTAL EXPENSES";
        }
        public ReportAttribute TotalDepartmentalIncome => new ReportAttribute() { Label = "TOTAL DEPARTMENTAL INCOME", Value = new Currency(Sum) };
        public void Rooms(decimal? money) => AddKey("Rooms", money);
        public void FoodAndBeverage(decimal? money) => AddKey("Food and Beverage", money);
        public void OtherOperatedDepartment(decimal? money) => AddKey("Other Operated Departments", money);
        public void TotalDepartmentalExpenses(decimal? money) => AddKey("TOTAL DEPARTMENTAL EXPENSES", money);
    }

    public class FoodBeverage : ReportAttribute
    {
        public void Restaurants(decimal? money) => AddKey("Restaurants", money);
        public void Bars(decimal? money) => AddKey("Bars", money);

        public void RoomService(decimal? money) => AddKey("Room Service", money);

        public void BanquetCartering(decimal? money) => AddKey("Banquet & Cartering", money);
        public void MeetingRooms(decimal? money) => AddKey("Meeting Rooms", money);
    }

    public class OtherOperatedDocs : ReportAttribute
    {
        public void GolfCourse(decimal? money) => AddKey("Golf Course", money);
        public void Spa(decimal? money) => AddKey("Spa", money);
        public void FitnessCenter(decimal? money) => AddKey("Fitness Center", money);
        public void BusinessCenter(decimal? money) => AddKey("Business Center", money);
        public void OtherRecreationFacilities(decimal? money) => AddKey("Other Recreation Facilities", money);
        public void Entertainment(decimal? money) => AddKey("Entertainment", money);

    }

    public class FixedCharges : ReportAttribute
    {
        public FixedCharges()
        {
            Label = "FIXED CHARGES";
        }

        public void PropertyAndOtherTax(decimal? value) => AddKey("Property and Other Taxes", value);
        public void Insurance(decimal? value) => AddKey("Insurance", value);
        public void Interest(decimal? value) => AddKey("Interest", value);
        public void DepreciationAmortization(decimal? value) => AddKey("Depreciation & amortization", value);
        public void TotalFixedCharges(decimal? value) => AddKey("Total Fixed Charges", value);
    }

    public class UndistOperatingExpenses : ReportAttribute
    {
        public UndistOperatingExpenses()
        {
            this.Label = "UNDISTRIBUTED OPERATING EXPENSES";
        }

        public void Adminstrative(decimal? value) => AddKey("Administrative and General", value);
        public void SalesAndMarketing(decimal? value) => AddKey("Sales and Marketing", value);
        public void CostOfUtilizingChannels(decimal? value) => AddKey("Cost of utilizing distribution channels", value);
        public void TotalSaleAndMarketing(decimal? value) => AddKey("TOTAL Sales and Marketing", value);
        public void PropertyOperation(decimal? value) => AddKey("Property Operation and Maintenance", value);
        public void Utilities(decimal? value) => AddKey("Utilities", value);
        public void TotalUnderdistributed(decimal? value) => AddKey("TOTAL UNDISTRIBUTED OPERATING EXPENSES", value);
    }

}
