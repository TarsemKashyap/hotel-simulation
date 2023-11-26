import { ReportCommon,currentAssests,ReportAttribute,propertyEquipement,libilitiesAndOwnerEquity1 } from "./ReportCommon.model";


export interface CashFlowReportResponse {
   
    netIncome :ReportAttribute,
    netCashFlow:ReportAttribute,
    currentCashBalnce:ReportAttribute,
    
}

