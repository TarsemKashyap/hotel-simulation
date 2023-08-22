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

export const reportRoutes: Routes = [
  {

    path: 'report',
    children: [
      { path: ':id/list', component: ReportListComponent },
      { path: ':id/objective-report', component: ObjectiveReportComponent },
      { path: ':id/performance-report', component: PerformanceComponent },
      { path: ':id/income-report',component:IncomeComponent},
      { path: ':id/balance-report',component:BalanceComponent},
      { path: ':id/cashflow-report',component:CashflowComponent}
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
