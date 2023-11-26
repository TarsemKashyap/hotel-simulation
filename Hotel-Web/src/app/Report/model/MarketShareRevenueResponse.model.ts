import { ReportCommon,occupancyReportAttribute,IoccupancyBySegment } from "./ReportCommon.model";


export interface MarketShareRevenueReportResponse {
   
    occupancyBySegment:IoccupancyBySegment[],
    overAllPercentages:occupancyReportAttribute[]   
}

