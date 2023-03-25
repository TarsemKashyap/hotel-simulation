import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InstructorComponent } from './instructor/instructor.component';
import { AddClassComponent } from '../shared/class';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: DashboardComponent,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'instructor/create', component: InstructorComponent },
      { path: 'class/add', component: AddClassComponent },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRouteModule {}
