import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { ColDef, IRowNode } from 'ag-grid-community';
import { Utility } from '../../utility';
import { ClassMapping, ClassSession } from '../model/classSession.model';
import { ClassService } from '../class.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GridActionComponent } from '../grid-action/grid-action.component';
import { GridActionParmas, RowAction } from '../grid-action/grid-action.model';

@Component({
  selector: 'app-add-removed-class',
  templateUrl: './add-removed-class.component.html',
  styleUrls: ['./add-removed-class.component.css'],
})
export class AddRemovedClassComponent {
 
  private datePipe = new DatePipe('en-US');
  isDefault: any;
  Titles: ClassSession[] = [];
  selectedTitle: ClassSession | undefined;
  myForm!: FormGroup;
  columnDefs: ColDef[] = [
    {
      field: 'code',
      tooltipValueGetter: () => 'Click to copy code',
      onCellClicked: (event) => {
        Utility.copyToClipboard(event.value);
        console.log('code', event.value);
        this.snackBar.open(`class code ${event.value} copied.`);
      },
    },
    { field: 'title' },
    {
      field: 'startDate',
      cellRenderer: (params: { value: string | number | Date }) =>
        this.datePipe.transform(params.value, 'dd-MM-yyyy'),
    },
    {
      field: 'endDate',
      cellRenderer: (params: { value: string | number | Date }) =>
        this.datePipe.transform(params.value, 'dd-MM-yyyy'),
    },
    { field: 'createdBy' },
    {
      field: 'action',
      headerName:'Set as default',
      cellRenderer: GridActionComponent,
      cellRendererParams: {
        actions: [
          {
            placeHolder: 'visibility',
            mode: 'icon',
            cssClass: 'hover:text-primary',
            onClick: this.setAsDefault(),
            hide: () => false,
          },
         
        ] as RowAction[],
      } as GridActionParmas,
    },
    {
      field: 'isDefault',
      headerName: 'Select as Default',
      onCellClicked: (event) => {
       
      },
    },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    menuTabs: ['filterMenuTab'],
    sortable: true,
  };

  $rows: ClassSession[] = [];

  constructor(
    private classService: ClassService,
    private router: Router,
    public snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.classService.studentByclass().subscribe((x) => {
      this.$rows = x.selectedClasses;
      this.Titles = x.availableClasses;
    });
  }

  Save() {
    if (!this.selectedTitle) {
      return;
    }
    this.classService.SaveClass(this.selectedTitle).subscribe((response) => {
      this.snackBar.open('Student Assign Class Saved Successfully');
    });
  }
  setAsDefault(): ($event: Event, row: any) => void {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      this.classService
      .setAsDefault(row.data!)
      .subscribe((data) => {
        this.snackBar.open('isDefault set succesfully');
      });
    };
        
  }
}
