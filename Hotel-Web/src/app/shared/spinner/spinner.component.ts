import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import {
  ProgressSpinnerMode,
  MatProgressSpinnerModule,
} from '@angular/material/progress-spinner';
import { ConfirmDialogModel } from '../dialog/confirm-dialog.component';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css'],
  standalone: true,
  imports: [MatProgressSpinnerModule],
})
export class SpinnerComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: string) {
  }
}
