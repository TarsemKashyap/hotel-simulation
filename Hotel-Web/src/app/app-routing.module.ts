import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  AuthCheckGuard,
  canAccessReports,
  hasAdminRole,
  hasInstructorRole,
  hasStudentRole,
} from './shared/auth.gurad';

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
    canActivate: [AuthCheckGuard, hasAdminRole],
  },
  {
    path: 'student',
    loadChildren: () =>
      import('./student/student.module').then((x) => x.StudentModule),
    canActivate: [AuthCheckGuard, hasStudentRole],
  },
  {
    path: 'report/:id',
    loadChildren: () =>
      import('./Report/report.module').then((x) => x.ReportModule),
    canActivate: [AuthCheckGuard, canAccessReports],
  },
  {
    path: 'instructor',
    loadChildren: () =>
      import('./instructor/instructor.module').then((x) => x.InstructorModule),
    canActivate: [AuthCheckGuard, hasInstructorRole],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
