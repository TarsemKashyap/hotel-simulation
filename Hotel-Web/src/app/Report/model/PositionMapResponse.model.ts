import { ReportCommon,PositionMapAttribute,MarketExpenditureAttribute } from "./ReportCommon.model";


export interface PositionMapReportResponse {
   
    groupRating:PositionMapAttribute[],
    segment:string  
}

