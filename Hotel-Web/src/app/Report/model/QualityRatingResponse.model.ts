import { ReportCommon,QualityRatingAttribute } from "./ReportCommon.moel";


export interface QualityRatingReportResponse {
   
    attributes:QualityRatingAttribute[],   
    overAll:QualityRatingAttribute,
    segments:QualityRatingAttribute[]

}

