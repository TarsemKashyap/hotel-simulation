import { ReportCommon,currentAssests,ReportAttribute,propertyEquipement,libilitiesAndOwnerEquity1 } from "./ReportCommon.moel";


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

