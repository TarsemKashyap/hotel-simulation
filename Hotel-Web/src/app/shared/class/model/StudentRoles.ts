export interface StudentRoleGroupAssign {
  studentId: string;
  GroupId: number;
  classId: number;
  Roles: number[];
}

export enum StudentRoles {
  RevenueManager = 1,
  RetailOperationsManager = 2,
  FBManager = 3,
  GeneralManager = 4,
  RoomManager = 5,
  MarketingManager = 6,
}
