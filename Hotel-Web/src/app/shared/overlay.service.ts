import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SpinnerComponent } from './spinner/spinner.component';
import { Observable, observable, tap } from 'rxjs';

@Injectable()
export class OverlayService {
  private dialogRef: MatDialogRef<SpinnerComponent>;
  constructor(private dialog: MatDialog) {}

  open(message: string): MatDialogRef<SpinnerComponent> {
    return this.dialog.open(SpinnerComponent, {
      panelClass: 'transparent',
      disableClose: true,
      data: message,
    });
  }

  loader<T>(message: string, ob: Observable<T>): Observable<T> {
    const dialogRef = this.dialog.open(SpinnerComponent, {
      panelClass: 'transparent',
      disableClose: true,
      data: message,
    });
    return ob.pipe(tap({ finalize: () => dialogRef.close() }));
  }

  close() {
    this.dialogRef.close();
  }
}
