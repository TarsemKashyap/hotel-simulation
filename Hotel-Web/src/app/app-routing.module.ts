import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared';
import { AuthRouteData, checkAccessPermission } from './shared/auth.gurad';
import { AppRoles } from './public/account';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadChildren: () =>
      import('./public/public.module').then((x) => x.publicModule),
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./admin/admin.module').then((x) => x.AdminModule),
    canActivate: [checkAccessPermission],
    data: { roles: [AppRoles.Admin] } as AuthRouteData,
  },
  {
    path: 'student',
    loadChildren: () =>
      import('./student/student.module').then((x) => x.StudentModule),
    canActivate: [checkAccessPermission],
    data: { roles: [AppRoles.Student] } as AuthRouteData,
  },
  {
    path: 'report/:id',
    loadChildren: () =>
      import('./Report/report.module').then((x) => x.ReportModule),
    canActivate: [checkAccessPermission],
    data: { roles: [AppRoles.Student] } as AuthRouteData,
  },
  {
    path: 'instructor',
    loadChildren: () =>
      import('./instructor/instructor.module').then((x) => x.InstructorModule),
    canActivate: [checkAccessPermission],
    data: { roles: [AppRoles.Instructor] } as AuthRouteData,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
