import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { checkAccessPermission } from '../shared/auth.gurad';
import { ChangePasswordComponent } from '../admin';
import { InstructorDashboard } from './dashboard/instructor-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';
import { classRoute } from '../shared/class/class.module';
import { reportRoutes } from '../Report/report-routing.module';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: InstructorDashboard,
    children: [
      { path: 'class', redirectTo: 'class' },
      { path: 'change-password', component: ChangePasswordComponent },
      ...classRoute,
      ...reportRoutes
    ],
    canActivate: [checkAccessPermission],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InstructorRouteModule {}
