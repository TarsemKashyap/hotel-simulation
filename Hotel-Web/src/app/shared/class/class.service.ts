import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { SessionStore } from 'src/app/store';
import { ClassSession } from '.';
import { StudentList } from './model/studentList.model';
import { InstructorDto } from 'src/app/admin/instructor';
import { StudentGroupList, StudentRoles } from './model/Roles';
import { StudentRoleGroupAssign } from './model/StudentRoles';

@Injectable({
  providedIn: 'root',
})
export class ClassService {
  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  addClass(classSession: ClassSession): Observable<any> {
    return this.httpClient.post('class', classSession);
  }

  list(): Observable<ClassSession[]> {
    return this.httpClient.get<ClassSession[]>('class/list');
  }

  getClass(classId: number): Observable<any> {
    return this.httpClient.get<ClassSession[]>(
      `class/${classId}`
    );
  }

  classUpdate(id: any, update: ClassSession): Observable<any> {
    return this.httpClient.post(`class/editClass/${id}`, update);
  }

    deleteClass(classId: number) {
    return this.httpClient.delete(
      `class/delete/${classId}`
    );
  }

  studentClassMappingList(classId:any): Observable<StudentList[]> {
    return this.httpClient.get<StudentList[]>(`roleMapping/studentlist/${classId}`);
  }

  getStudentData(id: string): Observable<any> {
    return this.httpClient.get<StudentList[]>(
      `roleMapping/student/${id}`
    );
  }

  Roleslist(): Observable<StudentRoles[]> {
    return this.httpClient.get<StudentRoles[]>('roleMapping/list');
  }

  Grouplist(): Observable<StudentGroupList[]> {
    return this.httpClient.get<StudentGroupList[]>('roleMapping/studentGroups');
  }

  AddRoles(roles: StudentRoleGroupAssign): Observable<any> {
    return this.httpClient.post('roleMapping', roles);
  }
}
