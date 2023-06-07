import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MonthDto } from './month.model';
import { Observable } from 'rxjs';
import { QuarterlyMarketDto } from './quarterly-market.model';


@Injectable({
  providedIn: 'root'
})
export class MonthService {

  constructor(private httpClient: HttpClient) {}

  monthList(): Observable<MonthDto[]> {
    return this.httpClient.get<MonthDto[]>('month/list');
  }

  deleteUser(userId: string) {
    return this.httpClient.delete(
      `account/instructor/${userId}`
    );
  }
  quarterlyMarketList(): Observable<QuarterlyMarketDto[]> {
    return this.httpClient.get<QuarterlyMarketDto[]>('quarterlyMarket/list');
  }
}
