import { ReportCommon,AttributeAmentitesReportAttribute,AbstractDecimal } from "./ReportCommon.moel";


export interface AttributeAmentitesReportResponse {
   
    attributes:AttributeAmentitesReportAttribute[],  
    totalExpense:AbstractDecimal 
}

