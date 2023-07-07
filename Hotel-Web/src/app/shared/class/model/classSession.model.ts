export interface ClassSession {
  title: string;
  startDate: string;
  endDate: string;
  hotelsCount: number;
  roomInEachHotel: number;
  currentQuater: number;
  code: string;
  createdBy: string;
  groups: ClassGroup[];
  classId?: number;
}

export interface ClassGroup {
  groupId?: number;
  serial: number;
  name: string;
  balance: number;
  action: Action;
}

export enum Action {
  Updated = 0,
  Added = 1,
  Removed = 2,
}

export interface ClassMapping {
  title: string;
}

export interface ClassSessionUpdate extends ClassSession {}

export interface AddRemoveClassDto {
  selectedClasses: Array<ClassSession>;
  availableClasses: Array<ClassSession>;
}

export interface ClassInformation extends ClassSession {
  totalStudentCount : number;
}

export interface RoomAllocations {
  id:number;
  monthID : number;
  quarterNo : number;
  groupID : number;
  weekday : boolean;
  segment : string;
  roomsAllocated : number;
  actualDemand : number;
  roomsSold : number;
  confirmed : boolean;
  revenue : number;
}
