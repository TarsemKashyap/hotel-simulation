import { ReportCommon } from "./ReportCommon.moel";
import { StatisticsDto } from "./Statistics.model";

export interface PerformanceResponse {
    statstics : StatisticsDto;
    financialRatios : ReportCommon;
}
