import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassDto, MonthDto } from './month-calculation.model';
import { Observable } from 'rxjs';
//import { QuarterlyMarketDto } from './quarterly-market.model';
//MonthCalculationService

@Injectable({
  providedIn: 'root',
})
export class MonthCalculationService {
  constructor(private httpClient: HttpClient) {}

  monthList(apiBody: any): Observable<any> {
    return this.httpClient.post<MonthDto[]>(
      'Calculation/calculationList',
      apiBody
    );
  }
  monthCalculate(apiBody: any): Observable<any> {
    return this.httpClient.post<MonthDto[]>('Calculation/Calculation', apiBody);
  }

  quarterlyMarketList(): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>('month/list');
  }
  createNewMonth(apiBody: any): Observable<any> {
    return this.httpClient.post('month/Create', apiBody);
  }
  classInfo(classId: any): Observable<ClassDto> {
    return this.httpClient.get<ClassDto>('month/classInfo/' + classId);
  }
  monthInfo(classId: any, quarterno: any): Observable<MonthDto> {
    return this.httpClient.get<MonthDto>(
      'month/monthInfo/' + classId + '/' + quarterno
    );
  }
  updateMonthCompletedStatus(apiBody: any): Observable<any> {
    return this.httpClient.post('month/UpdateMonthCompletedStatus', apiBody);
  }
  UpdateClassStatus(apiBody: any): Observable<any> {
    return this.httpClient.post('month/UpdateClassStatus', apiBody);
  }
  //UpdateClassStatus
}
