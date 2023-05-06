import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent, SignupComponent } from './account';
import { HomeComponent } from './home/home.component';
import { CompleteComponent } from './account/PayPal/complete/complete.component';

export const appRoutes: Routes = [

    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent},
    { path: 'signup', component: SignupComponent },
    { path: 'payment/complete', component: CompleteComponent },
];

@NgModule({
    imports: [RouterModule.forChild(appRoutes)],
    exports: [RouterModule],
    bootstrap: [HomeComponent]

})
export class publicRoutingModule { }
