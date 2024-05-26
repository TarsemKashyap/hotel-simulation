import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared';
import {
  AuthCheckGuard,
  canAccessReports,
  hasAdminRole,
  hasInstructorRole,
  hasStudentRole,
} from './shared/auth.gurad';
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
   // canActivate: [AuthCheckGuard],
  },
  {
    path: 'student',
    loadChildren: () =>
      import('./student/student.module').then((x) => x.StudentModule),
   // canActivate: [AuthCheckGuard],
  },
  {
    path: 'report/:id',
    loadChildren: () =>
      import('./Report/report.module').then((x) => x.ReportModule),
  //  canActivate: [AuthCheckGuard,canAccessReports],
  },
  {
    path: 'instructor',
    loadChildren: () =>
      import('./instructor/instructor.module').then((x) => x.InstructorModule),
    //canActivate: [AuthCheckGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
