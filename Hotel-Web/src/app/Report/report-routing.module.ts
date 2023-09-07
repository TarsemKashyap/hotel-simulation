import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthRouteData, checkAccessPermission } from '../shared/auth.gurad';
import { AppRoles } from '../public/account';
import { ReportListComponent } from './report-list/report-list.component';
import { ObjectiveReportComponent } from './objective-report/objective-report.component';
import { PerformanceComponent } from './performance/performance.component';
import {IncomeComponent} from './income/income.component';
import {BalanceComponent} from './balance/balance.component';
import {CashflowComponent} from './cashflow/cashflow.component';
import{OccupancyComponent} from './occupancy/occupancy.component';
import { AvgDailyRateComponent } from './avg-daily-rate/avg-daily-rate.component';
import { RevParGoparComponent } from './rev-par-gopar/rev-par-gopar.component';
import { RoomRateComponent } from './room-rate/room-rate.component';
import { MarketShareRevenueComponent } from './market-share-revenue/market-share-revenue.component';
import { MarketShareRoomSoldComponent } from './market-share-roomsold/market-share-roomsold.component';
import { MarketSharePositionAloneComponent } from './market-share-position-alone/market-share-position-alone.component';
import { AttributeAmentitiesComponent } from './attribute-amentities/attribute-amentities.component';
import { MarketExpenditureComponent } from './market-expenditure/market-expenditure.component';
import { PositionMapComponent } from './position-map/position-map.component';

export const reportRoutes: Routes = [
  {

    path: 'report',
    children: [
      { path: ':id/list', component: ReportListComponent },
      { path: ':id/objective-report', component: ObjectiveReportComponent },
      { path: ':id/performance-report', component: PerformanceComponent },
      { path: ':id/income-report',component:IncomeComponent},
      { path: ':id/balance-report',component:BalanceComponent},
      { path: ':id/cashflow-report',component:CashflowComponent},
      { path: ':id/occupancy-report',component:OccupancyComponent},
      { path: ':id/avg-daily-rate-report',component:AvgDailyRateComponent},
      { path: ':id/rev-par-gopar-report',component:RevParGoparComponent},
      { path: ':id/room-rate-report',component:RoomRateComponent},
      { path: ':id/market-share-revenue-report',component:MarketShareRevenueComponent},
      { path: ':id/market-share-roomsold-report',component:MarketShareRoomSoldComponent},
      { path: ':id/market-share-position-alone-report',component:MarketSharePositionAloneComponent},
      { path: ':id/attribute-amentities-report',component:AttributeAmentitiesComponent},
      { path: ':id/market-expenditure-report',component:MarketExpenditureComponent},
      { path: ':id/position-map-report',component:PositionMapComponent},
    ],
    canActivate:[checkAccessPermission],
    data: { role: AppRoles.Student } as AuthRouteData,
  },
];

@NgModule({
  imports: [RouterModule.forChild(reportRoutes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
