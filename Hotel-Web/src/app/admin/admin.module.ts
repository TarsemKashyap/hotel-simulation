import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { AdminRouteModule } from './admin-route.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    AdminRouteModule
  ],
  bootstrap:[DashboardComponent]
})
export class AdminModule { }
