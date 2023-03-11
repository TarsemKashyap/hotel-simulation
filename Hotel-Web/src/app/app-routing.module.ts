import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: "full" },
  { path: 'home', loadChildren: () => import("./public/public.module").then(x => x.publicModule) },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(x => x.AdminModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
