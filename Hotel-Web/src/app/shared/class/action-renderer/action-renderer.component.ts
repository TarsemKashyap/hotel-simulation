import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { ConfirmDialogComponent, ConfirmDialogModel } from '../../dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ClassService } from '../class.service';

@Component({
  selector: 'app-action-renderer',
  template: `
  <span>
    <span>{{ cellValue }}</span
    >&nbsp;
    <span (click)="edit()" class="material-icons">edit</span>
    <span (click)="delete()" class="material-icons">delete</span>
  </span>
`
})
export class ActionRendererComponent implements ICellRendererAngularComp{
  public cellValue!: string;
  classId : any;
  params : ICellRendererParams | undefined;

  constructor(
    private router: Router,
    private activatedRouter : ActivatedRoute,
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    private classService: ClassService,
  ) {}


// gets called once before the renderer is used
agInit(params: ICellRendererParams): void {
  console.log("params",params)
  this.params = params;
  this.cellValue = this.getValueToDisplay(params);
}

// gets called whenever the user gets the cell to refresh
refresh(params: ICellRendererParams) {
  // set value into cell again
  this.cellValue = this.getValueToDisplay(params);
  return true;
}

edit() {
    this.router.navigate([`class/edit/${this.params?.data.classId}`]);
}

delete() {
  const dialogData = new ConfirmDialogModel(
    'Delete',
    'Are you sure to delete?'
  );
  const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    minWidth: '400px',
    minHeight: '200px',
    data: dialogData,
  });
  dialogRef.afterClosed().subscribe((x) => {
    if (x) {
      this.classService.deleteClass(this.params?.data.classId).subscribe({
        next: () => {
          window.location.reload();
        },
        error: (err) => {
          this.snackBar.open('Error while deleting class.');
        },
      });
    }
  });
}

getValueToDisplay(params: ICellRendererParams) {
  return params.valueFormatted ? params.valueFormatted : params.value;
}
}
