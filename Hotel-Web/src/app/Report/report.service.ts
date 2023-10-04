import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MonthDto } from '../shared/class/create-month/month.model';
import { ClassGroup } from '../shared/class/model/classSession.model';
import { IncomeReportResponse } from './model/IncomeResponse.model';
import { BalanceReportResponse } from './model/BalanceResponse.model';
import { CashFlowReportResponse } from './model/CashFlowResponse.model';
import { OccupancyReportResponse } from './model/OccupancyResponse.model';
import { AvgDailyRateReportResponse } from './model/AvgDailyRateResponse.model';
import { RevParGopalReportResponse } from './model/RevParGoparResponse.model';
import { RoomRateReportResponse } from './model/RoomRateResponse.model';
import { MarketShareRevenueReportResponse } from './model/MarketShareRevenueResponse.model';
import { MarketShareRoomSoldReportResponse } from './model/MarketShareRoomSoldResponse.model';
import { MarketSharePositionAloneReportResponse } from './model/MarketSharePositionAloneResponse.model';
import { AttributeAmentitesReportResponse } from './model/AttributeAmentitesResponse.model';
import { MarketExpenditureReportResponse } from './model/MarketExpenditureResponse.model';
import { PositionMapReportResponse } from './model/PositionMapResponse.model';
import { QualityRatingReportResponse } from './model/QualityRatingResponse.model';
import { ReportParams } from './model/ReportParams.model';
import { GoalReportResponse } from './model/GoalReportResponse.model';
import { PerformanceInstReport, PerformanceResponse } from './model/PerformanceResponse.model';
@Injectable({
  providedIn: 'root',
})
export class ReportService {
  apiUrl: any;
  constructor(private httpClient: HttpClient) {}

  monthFilterList(classId: number): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>(
      `api/Report/monthFilterDetails/${classId}`
    );
  }

  groupFilterList(classId: number): Observable<ClassGroup[]> {
    return this.httpClient.get<ClassGroup[]>(
      `api/Report/groupFilterDetails/${classId}`
    );
  }

  objectiveReportDetails(
    reportParams: ReportParams
  ): Observable<GoalReportResponse[]> {
    return this.httpClient.post<GoalReportResponse[]>(
      `api/Report/goal`,
      reportParams
    );
  }

  performanceReportDetails(
    reportParams: ReportParams
  ): Observable<PerformanceResponse> {
    return this.httpClient.post<PerformanceResponse>(
      `api/Report/performance`,
      reportParams
    );
  }

  performanceInstReport(
    reportParams: ReportParams
  ): Observable<PerformanceInstReport> {
    return this.httpClient.post<PerformanceInstReport>(
      `api/ReportInstructor/performance`,
      reportParams
    );
  }

  incomeReportDetails(
    reportParams: ReportParams
  ): Observable<IncomeReportResponse> {
    return this.httpClient.post<IncomeReportResponse>(
      `api/Report/income`,
      reportParams
    );
  }
  balanceReportDetails(
    reportParams: ReportParams
  ): Observable<BalanceReportResponse> {
    return this.httpClient.post<BalanceReportResponse>(
      `api/Report/balance`,
      reportParams
    );
  }
  cashFlowReportDetails(
    reportParams: ReportParams
  ): Observable<CashFlowReportResponse> {
    return this.httpClient.post<CashFlowReportResponse>(
      `api/Report/cashflow`,
      reportParams
    );
  }
  occupancyReportDetails(
    reportParams: ReportParams
  ): Observable<OccupancyReportResponse> {
    return this.httpClient.post<OccupancyReportResponse>(
      `api/Report/occupancy`,
      reportParams
    );
  }
  avgDailyRateReportDetails(
    reportParams: ReportParams
  ): Observable<AvgDailyRateReportResponse> {
    return this.httpClient.post<AvgDailyRateReportResponse>(
      `api/Report/avg-daily-rate`,
      reportParams
    );
  }
  revParGopalReportDetails(
    reportParams: ReportParams
  ): Observable<RevParGopalReportResponse> {
    return this.httpClient.post<RevParGopalReportResponse>(
      `api/Report/rev-par-gopar`,
      reportParams
    );
  }
  roomRateReportDetails(
    reportParams: ReportParams
  ): Observable<RoomRateReportResponse> {
    return this.httpClient.post<RoomRateReportResponse>(
      `api/Report/roomRate`,
      reportParams
    );
  }
  marketShareRevenueReportDetails(
    reportParams: ReportParams
  ): Observable<MarketShareRevenueReportResponse> {
    return this.httpClient.post<MarketShareRevenueReportResponse>(
      `api/Report/market-share/revenue`,
      reportParams
    );
  }
  marketShareRoomSoldReportDetails(
    reportParams: ReportParams
  ): Observable<MarketShareRoomSoldReportResponse> {
    return this.httpClient.post<MarketShareRoomSoldReportResponse>(
      `api/Report/market-share/roomsold`,
      reportParams
    );
  }
  marketSharePositionAloneReportDetails(
    reportParams: ReportParams
  ): Observable<MarketSharePositionAloneReportResponse> {
    return this.httpClient.post<MarketSharePositionAloneReportResponse>(
      `api/Report/market-share/position-alone`,
      reportParams
    );
  }
  marketShareAttributeAmentitesReportDetails(
    reportParams: ReportParams
  ): Observable<AttributeAmentitesReportResponse> {
    return this.httpClient.post<AttributeAmentitesReportResponse>(
      `api/Report/attribute-amentities`,
      reportParams
    );
  }
  marketExpenditureReportDetails(
    reportParams: ReportParams
  ): Observable<MarketExpenditureReportResponse> {
    return this.httpClient.post<MarketExpenditureReportResponse>(
      `api/Report/market-expenditure`,
      reportParams
    );
  }
  positionMapReportDetails(
    reportParams: ReportParams
  ): Observable<PositionMapReportResponse> {
    return this.httpClient.post<PositionMapReportResponse>(
      `api/Report/position-map`,
      reportParams
    );
  }
  qualityRatingReportDetails(
    reportParams: ReportParams
  ): Observable<QualityRatingReportResponse> {
    return this.httpClient.post<QualityRatingReportResponse>(
      `api/Report/quality-rating`,
      reportParams
    );
  }
  numberWithCommas(x: any) {
    x = Math.round(x);
    var parts = x.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
  }
  decimalnumberWithCommas(x: any) {
    x = this.numberToDecimal(x);
    var parts = x.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
  }
  numberToDecimal(x: any) {
    return x.toFixed(2);
  }
}
