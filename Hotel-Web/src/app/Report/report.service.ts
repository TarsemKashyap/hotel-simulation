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
import { OccupancyReportResponse} from './model/OccupancyResponse.model';
import {AvgDailyRateReportResponse} from './model/AvgDailyRateResponse.model';
import {RevParGopalReportResponse} from './model/RevParGoparResponse.model';
import {RoomRateReportResponse} from './model/RoomRateResponse.model';
import {MarketShareRevenueReportResponse} from './model/MarketShareRevenueResponse.model';
import {MarketShareRoomSoldReportResponse} from './model/MarketShareRoomSoldResponse.model';
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
  avgDailyRateReportDetails(reportParams:ReportParams): Observable<AvgDailyRateReportResponse> {
    return this.httpClient.post<AvgDailyRateReportResponse>(`api/Report/avg-daily-rate`,reportParams);
  }
  revParGopalReportDetails(reportParams:ReportParams): Observable<RevParGopalReportResponse> {
    return this.httpClient.post<RevParGopalReportResponse>(`api/Report/rev-par-gopar`,reportParams);
  }
  roomRateReportDetails(reportParams:ReportParams): Observable<RoomRateReportResponse> {
    return this.httpClient.post<RoomRateReportResponse>(`api/Report/roomRate`,reportParams);
  }
  marketShareRevenueReportDetails(reportParams:ReportParams): Observable<MarketShareRevenueReportResponse> {
    return this.httpClient.post<MarketShareRevenueReportResponse>(`api/Report/market-share/revenue`,reportParams);
  }
  marketShareRoomSoldReportDetails(reportParams:ReportParams): Observable<MarketShareRoomSoldReportResponse> {
    return this.httpClient.post<MarketShareRoomSoldReportResponse>(`api/Report/market-share/roomsold`,reportParams);
  }
}
