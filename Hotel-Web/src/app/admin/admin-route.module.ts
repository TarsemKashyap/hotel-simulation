import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { canActivateHome } from '../shared/auth.gurad';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const OUTLET = "admin-outlet"
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: "full"
  },
  {
    path: 'dashboard',
    title: '',
    component: DashboardComponent,  // this is the component with the <router-outlet> in the template
  },

  { path: 'change-password', component: ChangePasswordComponent, outlet: OUTLET },
  {
    path: 'dashboard3', component: DashboardComponent
  }

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AdminRouteModule { }
