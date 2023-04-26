import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-action-renderer',
  template: `
  <span>
    <span>{{ cellValue }}</span
    >&nbsp;
    <button (click)="edit()">Edit</button>
    <button (click)="delete()">delete</button>
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
    this.router.navigate([`class/edit/${1}`]);
}

delete() {
  alert(`${this.cellValue} medals won!`);
}

getValueToDisplay(params: ICellRendererParams) {
  debugger
  return params.valueFormatted ? params.valueFormatted : params.value;
}
}
