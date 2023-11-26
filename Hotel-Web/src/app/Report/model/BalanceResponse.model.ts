import { ReportCommon,currentAssests,ReportAttribute,propertyEquipement,libilitiesAndOwnerEquity1 } from "./ReportCommon.model";


export interface BalanceReportResponse {
   
    currentAssests :currentAssests,
    propertyEquipement:propertyEquipement,
    otherAssets:ReportAttribute,
    totalAssests:ReportAttribute,
    libilitiesAndOwnerEquity:libilitiesAndOwnerEquity1,
    shareHolderEquity:ReportAttribute,
    retainedEarnings:ReportAttribute,
    totalShareHoldersEquity:ReportAttribute,
    totalLiablitiesAndEquity:ReportAttribute,
   

}

