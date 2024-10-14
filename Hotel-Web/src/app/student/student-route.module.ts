import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from '../admin';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';
import { AddRemovedClassComponent } from '../shared/class';
import {
  AuthCheckGuard,
  AuthGuard,
  hasStudentRole,
} from '../shared/auth.gurad';

import { reportRoutes } from '../Report/report-routing.module';
import {
  AttributeComponent,
  DecisionComponent,
  GoalSettingComponent,
  LoanComponent,
  MarketingComponent,
  PriceComponent,
  RoomComponent,
} from '../shared/decisions';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: StudentDashboard,
    canActivateChild: [AuthCheckGuard, hasStudentRole],
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'change-class', component: AddRemovedClassComponent },
      { path: 'manage-class', component: ClassListComponent },
      { path: 'decision', component: DecisionComponent },
      { path: 'room', component: RoomComponent },
      { path: 'attribute', component: AttributeComponent },
      { path: 'price', component: PriceComponent },
      { path: 'marketing', component: MarketingComponent },
      { path: 'goalSetting', component: GoalSettingComponent },
      { path: 'loan', component: LoanComponent },
      ...reportRoutes,
    ],
    canActivate: [AuthCheckGuard, hasStudentRole],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRouteModule {}
