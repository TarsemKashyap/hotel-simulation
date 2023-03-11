import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent, SignupComponent } from './account';
import { AccountService } from './account/account.service';
import { BannerComponent } from './banner/banner.component';
import { HeaderMenuComponent } from './header-menu/header-menu.component';
import { HomeComponent } from './home/home.component';
import { publicRoutingModule } from './public-routing.module';


@NgModule({
    imports: [publicRoutingModule, CommonModule, HttpClientModule, ReactiveFormsModule],
    exports: [HeaderMenuComponent,BannerComponent],
    declarations: [HeaderMenuComponent, BannerComponent, HomeComponent, SignupComponent, LoginComponent],
    providers: [AccountService],
    bootstrap: [HomeComponent]
})
export class publicModule { }
