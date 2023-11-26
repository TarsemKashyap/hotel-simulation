import { ReportCommon,MarketExpenditureReportAttribute,MarketExpenditureAttribute } from "./ReportCommon.model";


export interface MarketExpenditureReportResponse {
   
    segments:MarketExpenditureReportAttribute[],
    total:MarketExpenditureAttribute  
}

