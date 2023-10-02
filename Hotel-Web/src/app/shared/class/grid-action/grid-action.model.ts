import { ICellRendererParams, IRowNode, RowNode } from 'ag-grid-community';
import { Observable, Subject } from 'rxjs';

export interface RowAction {
  placeHolder: string;
  mode: ActionBtnType;
  cssClass?: string;
  hide: ($event: Event, row: any) => boolean;
  onClick: ($event: Event, row: any) => void;
  tooltip?:string;
}

declare type ActionBtnType = 'text' | 'icon';

export interface GridActionParmas extends ICellRendererParams {
  actions: RowAction[];
}
