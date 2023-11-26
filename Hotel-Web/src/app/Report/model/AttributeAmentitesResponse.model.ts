import { ReportCommon,AttributeAmentitesReportAttribute,AbstractDecimal } from "./ReportCommon.model";


export interface AttributeAmentitesReportResponse {
   
    attributes:AttributeAmentitesReportAttribute[],  
    totalExpense:AbstractDecimal 
}

