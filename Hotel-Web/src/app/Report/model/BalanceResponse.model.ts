import { ReportCommon,currentAssests,ReportAttribute,propertyEquipement,libilitiesAndOwnerEquity } from "./ReportCommon.model";


export interface BalanceReportResponse {
   
    currentAssests :currentAssests,
    propertyEquipement:propertyEquipement,
    otherAssets:ReportAttribute,
    totalAssests:ReportAttribute,
    libilitiesAndOwnerEquity:libilitiesAndOwnerEquity,
    shareHolderEquity:ReportAttribute,
    retainedEarnings:ReportAttribute,
    totalShareHoldersEquity:ReportAttribute,
    totalLiablitiesAndEquity:ReportAttribute,
   

}

