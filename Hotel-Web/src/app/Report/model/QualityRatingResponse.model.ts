import { ReportCommon,QualityRatingAttribute } from "./ReportCommon.model";


export interface QualityRatingReportResponse {
   
    attributes:QualityRatingAttribute[],   
    overAll:QualityRatingAttribute,
    segments:QualityRatingAttribute[]

}

