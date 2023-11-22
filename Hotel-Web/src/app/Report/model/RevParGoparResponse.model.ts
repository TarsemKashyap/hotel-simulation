import { ReportCommon,ReportAttribute,avgdailyrateReportAttribute } from "./ReportCommon.model";


export interface RevParGopalReportResponse {
   
    goPar :avgdailyrateReportAttribute,
    overAll:avgdailyrateReportAttribute,
    overAllChild:avgdailyrateReportAttribute[],
    totalRevpar:avgdailyrateReportAttribute
    
}

