import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { classRoute } from '../shared/class/class.module';
import { canActivateHome } from '../shared/auth.gurad';
import { ChangePasswordComponent } from '../admin';
import { InstructorDashboard } from './dashboard/instructor-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: InstructorDashboard,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'class/manage-class', component: ClassListComponent },
    ],
    //canActivate: [canActivateHome],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InstructorRouteModule {}
