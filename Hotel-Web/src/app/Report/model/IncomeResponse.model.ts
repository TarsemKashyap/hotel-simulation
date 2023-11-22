import { ReportCommon,IncomeReport,revenue,departmentalExpenses,ReportAttribute } from "./ReportCommon.model";


export interface IncomeReportResponse {
   
    revenue :revenue,
    departmentalExpenses:ReportAttribute,
    totalDepartIncome:ReportAttribute,
    undistOperatingExpenses:ReportAttribute,
    grossOperatingProfit:ReportAttribute,
    managmentFees:ReportAttribute,
    incomeBeforeFixedCharges:ReportAttribute,
    fixedCharges:ReportAttribute,
    netOperatingIncome:ReportAttribute,
    incomeTax:ReportAttribute,
    netIncome:ReportAttribute

}

