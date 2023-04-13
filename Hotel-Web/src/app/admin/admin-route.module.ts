import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { classRoute } from '../shared/class/class.module';
import { InstructorComponent, InstructorListComponent } from './instructor';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: DashboardComponent,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'instructor/create', component: InstructorComponent },
      { path: 'instructor/list', component: InstructorListComponent },
      ...classRoute
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRouteModule {}
