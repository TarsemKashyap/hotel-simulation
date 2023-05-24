import { StudentRoles } from "./Roles";

export interface StudentGroupRoles {
    studentId: string;
    GroupId: number;
    roleIds: StudentRoles[];
  }