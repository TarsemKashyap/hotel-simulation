import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { classRoute } from '../shared/class/class.module';
import { InstructorComponent, InstructorEditComponent, InstructorListComponent } from './instructor';
import { PaypalInitiatedPageComponent } from '../public/account/PayPal/paypal-initiated-page/paypal-initiated-page.component';
import { canActivateHome } from '../shared/auth.gurad';

const routes: Routes = [
  {
    path: '',
    title: '',
    component: DashboardComponent,
    children: [
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'instructor/create', component: InstructorComponent },
      { path: 'instructor/list', component: InstructorListComponent },
      { path: 'instructor/edit/:id', component: InstructorEditComponent },
      { path: 'Paypal/paypal-initiated-page/:id', component: PaypalInitiatedPageComponent },
      ...classRoute
    ],
    //canActivate: [canActivateHome],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRouteModule {}
