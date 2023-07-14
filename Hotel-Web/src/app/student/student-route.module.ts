import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from '../admin';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ClassListComponent } from '../shared/class/class-list/class-list.component';
import { AddRemovedClassComponent } from '../shared/class';
import { checkAccessPermission } from '../shared/auth.gurad';
import { DecisionComponent } from './decision/decision.component';
import { RoomComponent } from './room/room.component';
import { AttributeComponent } from './attribute/attribute.component';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: StudentDashboard,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'change-class', component: AddRemovedClassComponent },
      { path: 'manage-class', component: ClassListComponent },
      { path: 'decision', component: DecisionComponent },
      { path: 'room', component: RoomComponent },
      { path: 'attribute', component: AttributeComponent },
    ],
    canActivate:[]
  },
];



@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRouteModule {}
