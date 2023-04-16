import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionStore } from '../store/session.store';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ClassModule } from './class/class.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TextboxEditor } from './editors';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../material.module';
import { ConfirmDialogComponent } from './dialog/confirm-dialog.component';

@NgModule({
  declarations: [TextboxEditor, ConfirmDialogComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
  ],
  exports: [ClassModule,ConfirmDialogComponent],
  providers: [SessionStore, JwtHelperService],
})
export class SharedModule {}
