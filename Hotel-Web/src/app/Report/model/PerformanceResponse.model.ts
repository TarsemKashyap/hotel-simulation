import { ReportCommon } from './ReportCommon.model';
import { StatisticsDto } from './Statistics.model';

export interface PerformanceResponse {
  statstics: StatisticsDto;
  financialRatios: ReportCommon;
}

export interface PerformanceInstReport {
  statstics: StatisticsDto[];
  financialRatio: ReportCommon[];
}

export interface IValueFormat {
  format: string;
  value: number;
}
