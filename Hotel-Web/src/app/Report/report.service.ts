import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MonthDto } from '../shared/class/create-month/month.model';
import { ClassGroup } from '../shared/class/model/classSession.model';
import { ReportParams } from './model/ReportParams.model';
import { GoalReportResponse } from './model/GoalReportResponse.model';
import { PerformanceResponse } from './model/PerformanceResponse.model';
import { IncomeReportResponse } from './model/IncomeResponse.model';
import { BalanceReportResponse } from './model/BalanceResponse.model';
import { CashFlowReportResponse } from './model/CashFlowResponse.model';
import {OccupancyReportResponse} from './model/OccupancyResponse.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  monthFilterList(classId:number): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>(`api/Report/monthFilterDetails/${classId}`);
  }

  groupFilterList(classId:number): Observable<ClassGroup[]> {
    return this.httpClient.get<ClassGroup[]>(`api/Report/groupFilterDetails/${classId}`);
  }

  objectiveReportDetails(reportParams:ReportParams): Observable<GoalReportResponse[]> {
    return this.httpClient.post<GoalReportResponse[]>(`api/Report/goal`,reportParams);
  }

  performanceReportDetails(reportParams:ReportParams): Observable<PerformanceResponse> {
    return this.httpClient.post<PerformanceResponse>(`api/Report/performance`,reportParams);
  }
  
  incomeReportDetails(reportParams:ReportParams): Observable<IncomeReportResponse> {
    return this.httpClient.post<IncomeReportResponse>(`api/Report/income`,reportParams);
  }
  balanceReportDetails(reportParams:ReportParams): Observable<BalanceReportResponse> {
    return this.httpClient.post<BalanceReportResponse>(`api/Report/balance`,reportParams);
  }
  cashFlowReportDetails(reportParams:ReportParams): Observable<CashFlowReportResponse> {
    return this.httpClient.post<CashFlowReportResponse>(`api/Report/cashflow`,reportParams);
  }
  occupancyReportDetails(reportParams:ReportParams): Observable<OccupancyReportResponse> {
    return this.httpClient.post<OccupancyReportResponse>(`api/Report/occupancy`,reportParams);
  }
}
