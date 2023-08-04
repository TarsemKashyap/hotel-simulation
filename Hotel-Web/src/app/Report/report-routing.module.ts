import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthRouteData, checkAccessPermission } from '../shared/auth.gurad';
import { AppRoles } from '../public/account';
import { ReportListComponent } from './report-list/report-list.component';
import { ObjectiveReportComponent } from './objective-report/objective-report.component';

export const reportRoutes: Routes = [
  {

    path: 'report',
    children: [
      { path: ':id/list', component: ReportListComponent },
      { path: ':id/objective-report', component: ObjectiveReportComponent },
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
