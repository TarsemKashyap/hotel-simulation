export interface DemandReportModel {
  hotelDemand: Segment[];
  marketDemand: Segment[];
}

export interface Segment {
  segment: string;
  weekDay: WeekDay;
  weekEnd: WeekEnd;
}

export interface WeekDay {
  label: string;
  roomAllocated: number;
  roomDemanded: number;
  roomSold: number;
  deficit: number;
}

export interface WeekEnd {
  label: string;
  roomAllocated: number;
  roomDemanded: number;
  roomSold: number;
  deficit: number;
}
