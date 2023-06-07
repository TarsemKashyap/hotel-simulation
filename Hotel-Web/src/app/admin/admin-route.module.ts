import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { classRoute } from '../shared/class/class.module';
import { InstructorComponent, InstructorEditComponent, InstructorListComponent } from './instructor';
import { canActivateHome } from '../shared/auth.gurad';
import {CreateMonthComponent } from './instructor/create-month/create-month.component';

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
      ...classRoute
    ],
    //canActivate: [canActivateHome],
  },
  {
    path:'dashbord',
    component:DashboardComponent,
    children: [
      { path: '', component: CreateMonthComponent }
    ]
  },
  {
    path: 'createmonth',
    component: DashboardComponent,
    children: [
      { path: '', component: CreateMonthComponent }
    ]
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRouteModule {}
