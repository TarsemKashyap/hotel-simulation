import { ReportCommon,occupancyReportAttribute,IoccupancyBySegment } from "./ReportCommon.moel";


export interface MarketShareRevenueReportResponse {
   
    occupancyBySegment:IoccupancyBySegment[],
    overAllPercentages:occupancyReportAttribute[]   
}

