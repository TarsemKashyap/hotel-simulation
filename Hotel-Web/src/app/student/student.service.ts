import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentRoles } from '../shared/class/model/Roles';
import { RoomList } from '../shared/class/model/RoomList';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  StudentRoleslist(req:{studentId : string}): Observable<StudentRoles[]> {
    return this.httpClient.post<StudentRoles[]>('roleMapping/studentRolelist',req);
  }

 

  RoomList(): Observable<any> {
    return this.httpClient.get<RoomList[]>(
      `roleMapping/RoomList`
    );
  }

}
