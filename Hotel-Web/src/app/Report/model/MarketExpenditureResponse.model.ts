import { ReportCommon,MarketExpenditureReportAttribute,MarketExpenditureAttribute } from "./ReportCommon.moel";


export interface MarketExpenditureReportResponse {
   
    segments:MarketExpenditureReportAttribute[],
    total:MarketExpenditureAttribute  
}

