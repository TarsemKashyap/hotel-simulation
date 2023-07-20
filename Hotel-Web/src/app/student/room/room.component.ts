import { Component } from '@angular/core';
import { ColDef,IAggFuncParams, StatusPanelDef  } from 'ag-grid-community';
import { RoomList } from 'src/app/shared/class/model/RoomList';
import { Utility } from 'src/app/shared/utility';
import { StudentService } from '../student.service';
import { CustomTooltip, NumericEditor } from 'src/app/shared/editors';
import { CellRenderComponent } from 'src/app/shared/render';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent {
  isSaveAttempted = false;
  $roomListArr: RoomList[] = [];
  private gridColumnApi:any;
  private gridApi:any;
  weekdaySum:any = 0;
  cellRules = {
    'rag-red': (params:any) => this.isSaveAttempted && !params.value
  };

  columnDefs: ColDef[] = [
    {
      field: 'label',

    },
    { field: 'weekday',filter:"agNumberColumnFilter", valueParser: "Number(newValue)", cellClassRules: this.cellRules, editable: true, cellEditor: NumericEditor, singleClickEdit: true,  cellRenderer:this.validationShow, tooltipValueGetter: this.validationStatusRenderer,onCellValueChanged:this.sumAllfield
     , aggFunc: (params: IAggFuncParams) => {
      let sum = 0;
      params.values.forEach((value: number) => (sum += value));
      console.log("sum",sum)
    },
      },
    {
      field: 'weekend', editable: true, singleClickEdit: true, cellEditor: NumericEditor, tooltipComponent: CustomTooltip,
    },

  ];

  public statusBar: {
    statusPanels: StatusPanelDef[];
  } = {
    statusPanels: [
      {
        statusPanel: 'agAggregationComponent',
        statusPanelParams: {
        
          aggFuncs: ['sum', 'avg'],
        },
      },
    ],
  };
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    menuTabs: ['filterMenuTab'],
    sortable: true,
  };

  $rows: RoomList[] = [];

  ngOnInit(): void {

    this.roomList();
  }

  constructor(
    private studentService: StudentService,) {
  }
  sumAllfield(param:any){
    console.log('getter',param);
  }

  validationShow(params: any) {
    
    if (!params.value) {
      return "";
    }
    return params.value;
  }

  validationStatusRenderer(params: any) {
    console.log("validationStatusRenderer",params)
    let message = params.value  ? '' : 'Value not Empty';
    return message;
  }

  onGridReady(params:any) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    console.log('onGridReady');
  }

  onSave(): void {
    this.isSaveAttempted = true;
    this.gridApi.redrawRows();

    // validation
    let validationPass = true;

    this.$rows.forEach(row => {
      if (!row['Weekday']) {
        row['Weekday'] = '';

        validationPass = false;
      }
      if (!row['Weekend']) {
        row['Weekend'] = '';
        validationPass = false;
      }
    });

    alert(`Validation result: ${validationPass ? 'OK' : 'Failed'}`);
  }

  private roomList() {

    this.studentService
      .RoomList().subscribe((data) => {
        this.$rows = data;
      });
  }
}
