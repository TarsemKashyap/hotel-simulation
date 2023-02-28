import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent, SignupComponent } from './account';
import { AppComponent } from './app.component';

export const appRoutes: Routes = [
  { path: "", redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: AppComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
