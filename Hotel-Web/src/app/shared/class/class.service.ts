import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { SessionStore } from 'src/app/store';
import { ClassSession } from '.';
import { StudentList } from './model/studentList.model';
import { InstructorDto } from 'src/app/admin/instructor';
import { StudentGroupList, StudentRoleGroupRequest, StudentRoles } from './model/Roles';
import { StudentRoleGroupAssign } from './model/StudentRoles';
import { AddRemoveClassDto, ClassMapping } from './model/classSession.model';

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

  Roleslist(req:{studentId : string, classId : number}): Observable<StudentRoleGroupRequest> {
    return this.httpClient.post<StudentRoleGroupRequest>('roleMapping/list',req);
  }

  Grouplist(): Observable<StudentGroupList[]> {
    return this.httpClient.get<StudentGroupList[]>('roleMapping/studentGroups');
  }

  AddRoles(roles: StudentRoleGroupAssign): Observable<any> {
    return this.httpClient.post('roleMapping', roles);
  }

  getRoles(studentId: string): Observable<any> {
    return this.httpClient.get<StudentRoleGroupAssign[]>(
      `roleMapping/${studentId}`
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

  Roleslist(req:{studentId : string, classId : number}): Observable<StudentRoleGroupRequest> {
    return this.httpClient.post<StudentRoleGroupRequest>('roleMapping/list',req);
  }

  Grouplist(): Observable<StudentGroupList[]> {
    return this.httpClient.get<StudentGroupList[]>('roleMapping/studentGroups');
  }

  AddRoles(roles: StudentRoleGroupAssign): Observable<any> {
    return this.httpClient.post('roleMapping', roles);
  }

  getRoles(studentId: string): Observable<any> {
    return this.httpClient.get<StudentRoleGroupAssign[]>(
      `roleMapping/${studentId}`
    );
  }

  studentByclass(): Observable<AddRemoveClassDto> {
    return this.httpClient.get<AddRemoveClassDto>('studentClassMapping/studentlist');
  }

  setAsDefault(req:ClassSession): Observable<any> {
    return this.httpClient.post<ClassSession>('studentClassMapping/studentClassUpdate',req);
  }

  ClassTitlelist(): Observable<any> {
    return this.httpClient.get<ClassSession[]>('studentClassMapping/studentClasslist');
  }

  SaveClass(classSession: ClassMapping): Observable<any> {
    return this.httpClient.post('studentClassMapping/studentClassAssign', classSession);
  }
}
