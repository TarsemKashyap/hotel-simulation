import { ReportCommon,currentAssests,ReportAttribute,propertyEquipement,libilitiesAndOwnerEquity } from "./ReportCommon.model";


export interface CashFlowReportResponse {
   
    netIncome :ReportAttribute,
    netCashFlow:ReportAttribute,
    currentCashBalnce:ReportAttribute,
    
}

