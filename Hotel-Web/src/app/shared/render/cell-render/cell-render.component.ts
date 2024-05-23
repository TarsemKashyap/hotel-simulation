import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { CellRangeParams, ICellRendererParams } from 'ag-grid-community';
import { GridActionParmas, RowAction } from './cell-render.model';

@Component({
  selector: 'app-action-renderer',
  templateUrl: './cell-render.component.html',
})
export class CellRenderComponent implements ICellRendererAngularComp {
  public cellValue!: string;
 
  params:ICellRendererParams | undefined;
  
  constructor(
  ) {}

  // gets called once before the renderer is used
  agInit(params: any): void {
    this.params = params;
    this.cellValue = this.getValueToDisplay(params);
  }

  
  // gets called whenever the user gets the cell to refresh
  refresh(params: ICellRendererParams) {
    // set value into cell again
    this.cellValue = this.getValueToDisplay(params);
    return true;
  }

  getValueToDisplay(params: ICellRendererParams) {
    return params.valueFormatted ? params.valueFormatted : params.value;
  }
}
