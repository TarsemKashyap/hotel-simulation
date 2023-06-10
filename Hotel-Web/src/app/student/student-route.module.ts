import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { classRoute } from '../shared/class/class.module';
import { canActivateHome } from '../shared/auth.gurad';
import { ChangePasswordComponent } from '../admin';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';
import { AddRemovedClassComponent } from '../shared/class';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: StudentDashboard,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'manage-class', component: ClassListComponent },
      { path: 'class/add-change', component: AddRemovedClassComponent },

    ],
    //canActivate: [canActivateHome],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRouteModule {}
