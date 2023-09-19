import { ReportCommon,IoccupancyBySegment,occupancyReportAttribute } from "./ReportCommon.moel";


export interface OccupancyReportResponse {
   
    occupancyBySegment :IoccupancyBySegment[],
    overAllPercentages:occupancyReportAttribute[],
   
    
}

