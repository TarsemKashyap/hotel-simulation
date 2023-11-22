import {
  ReportCommon,
  IoccupancyBySegment,
  occupancyReportAttribute,
} from './ReportCommon.model';

export interface OccupancyReportResponse {
  occupancyBySegment: IoccupancyBySegment[];
  overAllPercentages: occupancyReportAttribute[];
}
