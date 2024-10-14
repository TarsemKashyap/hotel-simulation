import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentRoles } from '../shared/class/model/Roles';
import { RoomList } from '../shared/class/model/RoomList';
import {
  AttributeDecision,
  BalanceSheet,
  DefaultClassSession,
  Goal,
  MarketingDecision,
  PriceDecision,
  RoomAllocationDetails,
  RoomAllocations,
} from '../shared/class/model/classSession.model';
import { DecisionService } from '../shared/decisions/decision/decision.service';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  apiUrl: any;
  constructor(
    private httpClient: HttpClient,
  ) {}

  StudentRoleslist(): Observable<StudentRoles[]> {
    return this.httpClient.post<StudentRoles[]>(
      'roleMapping/studentRolelist',
      ''
    );
  }

  RoomAllocationList(
    defaultClass: DefaultClassSession
  ): Observable<RoomAllocationDetails> {
    return this.httpClient.post<RoomAllocationDetails>(
      'decision/RoomAllocationDetails',
      defaultClass
    );
  }

  AttributeDecisionList(
    defaultClass: DefaultClassSession
  ): Observable<AttributeDecision[]> {
    return this.httpClient.post<AttributeDecision[]>(
      'decision/AttributeDecisionDetails',
      defaultClass
    );
  }

  AttributeDecisionUpdate(
    attributeDecisionList: AttributeDecision[]
  ): Observable<any> {
    return this.httpClient.post(
      `roleMapping/UpdateAttributeDecision`,
      attributeDecisionList
    );
  }

  RoomAllocationUpdate(roomAllocationList: RoomAllocations[]): Observable<any> {
    return this.httpClient.post(
      `roleMapping/UpdateRoomAllocationDtls`,
      roomAllocationList
    );
  }

  MarketingDetails(
    defaultClass: DefaultClassSession
  ): Observable<MarketingDecision[]> {
    return this.httpClient.post<MarketingDecision[]>(
      'decision/MarketingDetails',
      defaultClass
    );
  }

  GoalDetails(defaultClass: DefaultClassSession): Observable<Goal> {
    return this.httpClient.post<Goal>(
      'decision/GoalSettingDetails',
      defaultClass
    );
  }

  UpdateBalanceSheetDetails(balanceSheet: BalanceSheet): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateBalanceSheet`, balanceSheet);
  }

  BalanceSheetDetails(
    defaultClass: DefaultClassSession
  ): Observable<BalanceSheet> {
    return this.httpClient.post<BalanceSheet>(
      'decision/GetBalanceSheet',
      defaultClass
    );
  }

  UpdateGoalDetails(goalList: Goal): Observable<any> {
    return this.httpClient.post(`roleMapping/UpdateGoalSetting`, goalList);
  }

  UpdateMarketingDetails(
    marketingDecisionList: MarketingDecision[]
  ): Observable<any> {
    return this.httpClient.post(
      `roleMapping/UpdateMarketingDetails`,
      marketingDecisionList
    );
  }

  PriceDecisionList(
    defaultClass: DefaultClassSession
  ): Observable<PriceDecision[]> {
    return this.httpClient.post<PriceDecision[]>(
      'decision/PriceDecisionDetails',
      defaultClass
    );
  }

  PriceDecisionUpdate(priceDecisionList: PriceDecision[]): Observable<any> {
    return this.httpClient.post(
      `roleMapping/UpdatePriceDecision`,
      priceDecisionList
    );
  }

  RoomList(): Observable<any> {
    return this.httpClient.get<RoomList[]>(`roleMapping/RoomList`);
  }
}
