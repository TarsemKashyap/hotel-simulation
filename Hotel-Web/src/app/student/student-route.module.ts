import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from '../admin';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';
import { AddRemovedClassComponent } from '../shared/class';
import { checkAccessPermission } from '../shared/auth.gurad';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: StudentDashboard,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'change-class', component: AddRemovedClassComponent },
    ],
    canActivate:[checkAccessPermission]
  },
];



@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRouteModule {}
