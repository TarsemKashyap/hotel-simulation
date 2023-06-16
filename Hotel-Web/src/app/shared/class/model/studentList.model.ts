import { ClassInformation, ClassSession } from "./classSession.model";

export interface StudentList {
    id : string;
    FirstName: string;
    LastName: string;
    title: string;
    GroupName: string;
    Email: string;
    Institute: string;
    startDate: string;
    endDate: string;
    totalStudents: number;
  }

  export interface ClassOverview {
    studentClassMappingDto :StudentList[];
    classSessionDto: ClassInformation;
  }