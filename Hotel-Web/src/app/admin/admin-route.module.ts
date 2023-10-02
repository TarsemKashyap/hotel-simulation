import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { classRoute } from '../shared/class/class.module';
import {
  InstructorComponent,
  InstructorEditComponent,
  InstructorListComponent,
} from './instructor';
import { AuthRouteData, checkAccessPermission } from '../shared/auth.gurad';
import { reportRoutes } from '../Report/report-routing.module';
import { AppRoles } from '../public/account';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: DashboardComponent,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'instructor/create', component: InstructorComponent },
      { path: 'instructor/list', component: InstructorListComponent },
      { path: 'instructor/edit/:id', component: InstructorEditComponent },
      ...classRoute,
      ...reportRoutes
    ],
    canActivate: [checkAccessPermission],
    data:{ roles:[AppRoles.Admin] } as AuthRouteData
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRouteModule {}
