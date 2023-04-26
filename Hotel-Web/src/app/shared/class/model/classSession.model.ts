export interface ClassSession {
  title: string;
  startDate: string;
  endDate: string;
  hotelsCount: number;
  roomInEachHotel: number;
  currentQuater: number;
  code: string;
  createdBy:string;
  groups:ClassGroup[]
}

export interface ClassGroup {
  groupId?: number;
  serial: number;
  name: string;
  balance:number
}

export interface ClassSessionUpdate extends ClassSession {
  added:ClassGroup[],
  removed: ClassGroup[]
}