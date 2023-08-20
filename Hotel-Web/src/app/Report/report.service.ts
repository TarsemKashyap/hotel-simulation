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

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  monthFilterList(classId:number): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>(`Reports/monthFilterDetails/${classId}`);
  }

  groupFilterList(classId:number): Observable<ClassGroup[]> {
    return this.httpClient.get<ClassGroup[]>(`Reports/groupFilterDetails/${classId}`);
  }

  objectiveReportDetails(reportParams:ReportParams): Observable<GoalReportResponse[]> {
    return this.httpClient.post<GoalReportResponse[]>(`Reports/goal`,reportParams);
  }

  performanceReportDetails(reportParams:ReportParams): Observable<PerformanceResponse> {
    return this.httpClient.post<PerformanceResponse>(`Reports/performance`,reportParams);
  }
  
  incomeReportDetails(reportParams:ReportParams): Observable<IncomeReportResponse> {
    return this.httpClient.post<IncomeReportResponse>(`Reports/income`,reportParams);
  }
  balanceReportDetails(reportParams:ReportParams): Observable<BalanceReportResponse> {
    return this.httpClient.post<BalanceReportResponse>(`Reports/balance`,reportParams);
  }

}
