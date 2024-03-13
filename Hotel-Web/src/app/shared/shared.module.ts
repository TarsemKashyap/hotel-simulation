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
import { NumericEditor } from './editors';
import { CustomTooltip } from './editors';
import { CellRenderComponent } from './render';
import { SpinnerComponent } from './spinner/spinner.component';
import { OverlayService } from './overlay.service';

@NgModule({
  declarations: [
    TextboxEditor,
    ConfirmDialogComponent,
    NumericEditor,
    CustomTooltip,
    CellRenderComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
  ],
  exports: [ClassModule, ConfirmDialogComponent],
  providers: [SessionStore, JwtHelperService, OverlayService],
})
export class SharedModule {}
