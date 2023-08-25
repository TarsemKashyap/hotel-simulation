import { ReportCommon,ReportAttribute,avgdailyrateReportAttribute } from "./ReportCommon.moel";


export interface RevParGopalReportResponse {
   
    goPar :avgdailyrateReportAttribute,
    overAll:avgdailyrateReportAttribute,
    overAllChild:avgdailyrateReportAttribute[],
    totalRevpar:avgdailyrateReportAttribute
    
}

