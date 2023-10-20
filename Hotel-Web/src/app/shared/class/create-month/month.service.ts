import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassDto, MonthDto } from './month.model';
import { Observable } from 'rxjs';
//import { QuarterlyMarketDto } from './quarterly-market.model';

@Injectable({
  providedIn: 'root',
})
export class MonthService {
  constructor(private httpClient: HttpClient) {}

  monthList(): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>('month/list');
  }

  deleteUser(userId: string) {
    return this.httpClient.get(`account/instructor/${userId}`);
  }
  quarterlyMarketList(apiBody: any): Observable<MonthDto[]> {
    return this.httpClient.post<MonthDto[]>('month/list', apiBody);
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
