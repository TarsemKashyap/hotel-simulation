import { ReportCommon,roomRateReportAttribute } from "./ReportCommon.model";


export interface RoomRateReportResponse {
   
    direct:roomRateReportAttribute[],
    onlineTravelAgent:roomRateReportAttribute[],
    opaque:roomRateReportAttribute[],
    travelAgent:roomRateReportAttribute[]
    
}

