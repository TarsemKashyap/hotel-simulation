import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent, SignupComponent } from './account';
import { AccountService } from './account/account.service';
import { BannerComponent } from './banner/banner.component';
import { HeaderMenuComponent } from './header-menu/header-menu.component';
import { HomeComponent } from './home/home.component';
import { publicRoutingModule } from './public-routing.module';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { CompleteComponent, PaymentInitiatedPageComponent } from './student';
import { NgxPayPalModule } from 'ngx-paypal';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    publicRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    NgxPayPalModule,
   // BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
  ],
  exports: [HeaderMenuComponent, BannerComponent, SignupComponent],
  declarations: [
    HeaderMenuComponent,
    BannerComponent,
    HomeComponent,
    SignupComponent,
    LoginComponent,
    PaymentInitiatedPageComponent,
    CompleteComponent,
  ],
  providers: [AccountService],
  bootstrap: [HomeComponent],
})
export class publicModule {}
