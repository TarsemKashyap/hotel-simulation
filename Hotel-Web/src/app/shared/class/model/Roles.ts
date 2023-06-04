import { ClassGroup } from "./classSession.model";

export interface StudentRoles {
    id : number;
    roleName : string;
  }

  export interface StudentGroupList {
    id : string;
    name : string;
    action : number;
    balance: number;
    groupId : number;
    serial : number;
  }

  export interface StudentRoleGroupRequest {
    classGroups : ClassGroup[];
    studentRole : StudentRoles[];
    selectedRoles : StudentRoles[];
    selectedGroup : ClassGroup;
  }