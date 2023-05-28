import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared';
import { canActivateHome } from './shared/auth.gurad';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: "full" },
  { path: 'home', loadChildren: () => import("./public/public.module").then(x => x.publicModule) },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(x => x.AdminModule),canActivate:[canActivateHome] },
  { path: 'student', loadChildren: () => import('./student/student.module').then(x => x.StudentModule),canActivate:[canActivateHome] },
  { path: 'instructor', loadChildren: () => import('./instructor/instructor.module').then(x => x.InstructorModule),canActivate:[canActivateHome] },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
