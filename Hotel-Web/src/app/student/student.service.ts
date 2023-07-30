import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentRoles } from '../shared/class/model/Roles';
import { RoomList } from '../shared/class/model/RoomList';
import { AttributeDecision, BalanceSheet, Goal, MarketingDecision, PriceDecision, RoomAllocations } from '../shared/class/model/classSession.model';

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

  MarketingDetails(): Observable<MarketingDecision[]> {
    return this.httpClient.get<MarketingDecision[]>('roleMapping/MarketingDetails');
  }

  GoalDetails(): Observable<Goal> {
    return this.httpClient.get<Goal>('roleMapping/GoalSettingDetails');
  }

  UpdateBalanceSheetDetails( balanceSheet: BalanceSheet): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateBalanceSheet`, balanceSheet);
  }

  BalanceSheetDetails(): Observable<BalanceSheet> {
    return this.httpClient.get<BalanceSheet>('roleMapping/GetBalanceSheet');
  }

  UpdateGoalDetails( goalList: Goal): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateGoalSetting`, goalList);
  }
 
  UpdateMarketingDetails( marketingDecisionList: MarketingDecision[]): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateMarketingDetails`, marketingDecisionList);
  }

  PriceDecisionList(): Observable<PriceDecision[]> {
    return this.httpClient.get<PriceDecision[]>('roleMapping/PriceDecisionDetails');
  }
 
  PriceDecisionUpdate( priceDecisionList: PriceDecision[]): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdatePriceDecision`, priceDecisionList);
  }

  RoomList(): Observable<any> {
    return this.httpClient.get<RoomList[]>(
      `roleMapping/RoomList`
    );
  }

}
