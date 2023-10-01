import { ReportCommon,roomRateReportAttribute } from "./ReportCommon.moel";


export interface RoomRateReportResponse {
   
    direct:roomRateReportAttribute[],
    onlineTravelAgent:roomRateReportAttribute[],
    opaque:roomRateReportAttribute[],
    travelAgent:roomRateReportAttribute[]
    
}

