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
  balance:number;
  action: Action
}

export enum Action {
  Updated = 0,
  Added = 1,
  Removed = 2
}



export interface ClassSessionUpdate extends ClassSession {
  added:ClassGroup[],
  removed: ClassGroup[]
}