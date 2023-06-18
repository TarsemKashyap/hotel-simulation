import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { checkAccessPermission } from '../shared/auth.gurad';
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
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InstructorRouteModule {}
