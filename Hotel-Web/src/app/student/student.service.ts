import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentRoles } from '../shared/class/model/Roles';
import { RoomList } from '../shared/class/model/RoomList';
import { AttributeDecision, RoomAllocations } from '../shared/class/model/classSession.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  StudentRoleslist(): Observable<StudentRoles[]> {
    
    return this.httpClient.post<StudentRoles[]>('roleMapping/studentRolelist',"");
  }

  RoomAllocationList(): Observable<RoomAllocations[]> {
    return this.httpClient.get<RoomAllocations[]>('roleMapping/RoomAllocationDetails');
  }

  AttributeDecisionList(): Observable<AttributeDecision[]> {
    return this.httpClient.get<AttributeDecision[]>('roleMapping/AttributeDecisionDetails');
  }

  AttributeDecisionUpdate( attributeDecisionList: AttributeDecision[]): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateAttributeDecision`, attributeDecisionList);
  }

  RoomAllocationUpdate( roomAllocationList: RoomAllocations[]): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateRoomAllocationDtls`, roomAllocationList);
  }

 

  RoomList(): Observable<any> {
    return this.httpClient.get<RoomList[]>(
      `roleMapping/RoomList`
    );
  }

}
