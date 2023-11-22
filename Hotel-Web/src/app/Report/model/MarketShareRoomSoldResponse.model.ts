import { ReportCommon,occupancyReportAttribute,IoccupancyBySegment } from "./ReportCommon.model";


export interface MarketShareRoomSoldReportResponse {
   
    occupancyBySegment:IoccupancyBySegment[],
    overAllPercentages:occupancyReportAttribute[]   
}

