import { ReportCommon, roomRateReportAttribute } from './ReportCommon.model';

export interface RoomRateReportResponse {
  direct: RoomRateAgg;
  onlineTravelAgent: RoomRateAgg;
  opaque: RoomRateAgg;
  travelAgent: RoomRateAgg;
}

export interface RoomRateAgg {
  sumWeekdayRoomSold: Number;
  sumWeekEndRoomSold: Number;
  sumWeekdayCost: Number;
  sumWeekEndCost: Number;
  sumTotalCost: Number;
  segments: Array<roomRateReportAttribute>
}
