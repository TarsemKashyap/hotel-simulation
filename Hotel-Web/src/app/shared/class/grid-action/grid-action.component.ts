import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { GridActionParmas, RowAction } from './grid-action.model';

@Component({
  selector: 'app-action-renderer',
  templateUrl: './grid-action.component.html',
})
export class GridActionComponent implements ICellRendererAngularComp {
  public cellValue!: string;
  classId: any;
  params: GridActionParmas | undefined;
  actions: RowAction[] = [];
  constructor() {}

  // gets called once before the renderer is used
  agInit(params: GridActionParmas): void {
    this.params = params;
    this.cellValue = this.getValueToDisplay(params);
    this.actions = params.actions;
  }

  localClick($event: Event, action: RowAction) {
    action.onClick($event, this.params?.node);
  }

  hide(action: RowAction): boolean {
    return action.hide(this.params?.node);
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
