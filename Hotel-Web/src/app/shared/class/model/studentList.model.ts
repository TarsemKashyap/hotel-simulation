import { ClassInformation, ClassSession } from './classSession.model';
import { StudentRoles } from './Roles';

export interface StudentList {
  id: string;
  FirstName: string;
  LastName: string;
  title: string;
  GroupName: string;
  Email: string;
  Institute: string;
  startDate: string;
  endDate: string;
  totalStudents: number;
  classId:number,
  groupSerial:number
  roles:StudentRoles[]

}

export interface ClassOverview {
  studentClassMappingDto: StudentList[];
  classSessionDto: ClassInformation;
}
