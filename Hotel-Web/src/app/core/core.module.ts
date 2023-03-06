import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerComponent } from './banner/banner.component';
import { HeaderMenuComponent } from './header-menu/header-menu.component';


@NgModule({
  declarations: [BannerComponent, HeaderMenuComponent],
  imports: [
    CommonModule
  ],
  exports: [HeaderMenuComponent, BannerComponent]
})
export class CoreModule { }
