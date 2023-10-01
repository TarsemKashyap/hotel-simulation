import { ReportCommon,occupancyReportAttribute,IoccupancyBySegment } from "./ReportCommon.moel";


export interface MarketShareRoomSoldReportResponse {
   
    occupancyBySegment:IoccupancyBySegment[],
    overAllPercentages:occupancyReportAttribute[]   
}

