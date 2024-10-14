import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { ColDef, IRowNode } from 'ag-grid-community';
import { Utility } from '../../utility';
import { ClassMapping, ClassSession } from '../model/classSession.model';
import { ClassService } from '../class.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GridActionComponent } from '../grid-action/grid-action.component';
import { GridActionParmas, RowAction } from '../grid-action/grid-action.model';
import { ToastrService } from 'ngx-toastr';
import { valueOrDefault } from 'chart.js/dist/helpers/helpers.core';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-add-removed-class',
  templateUrl: './add-removed-class.component.html',
  styleUrls: ['./add-removed-class.component.css'],
})
export class AddRemovedClassComponent {
  private datePipe = new DatePipe('en-US');
  isDefault: any;
  Titles: ClassSession[] = [];
  selectedTitle: string | undefined;
  myForm!: FormGroup;
  columnDefs: ColDef[] = [
    {
      field: 'code',
      headerName: 'Class ID code',
      tooltipValueGetter: () => 'Click to copy code',
      onCellClicked: (event) => {
        Utility.copyToClipboard(event.value);
        this.snackBar.open(`class code ${event.value} copied.`);
      },
    },
    { field: 'title', headerName: 'Class Name' },
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
    // { field: 'createdBy' },
    {
      field: 'isDefaultSet',
      headerName: 'Class Status',
      cellRenderer: (param: { value: boolean }) => {
        return param.value
          ? 'Default Active Class'
          : 'Not Default Active Class';
      },
    },
    {
      field: 'action',
      headerName: 'Set as Default Active',
      cellRenderer: GridActionComponent,
      cellRendererParams: {
        actions: [
          {
            placeHolder: 'menu',
            mode: 'icon',
            onClick: this.setAsDefault(),
            hide: () => false,
            tooltip: 'Set as Default Active Class',
          },
        ] as RowAction[],
      } as GridActionParmas,
    },
    {
      field: 'report',
      headerName: 'Reports',
      cellRenderer: GridActionComponent,
      cellRendererParams: {
        actions: [
          {
            placeHolder: 'assignment',
            mode: 'icon',
            onClick: this.loadReport(),
            hide: () => false,
            tooltip: 'View',
          },
        ] as RowAction[],
      } as GridActionParmas,
    },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    sortable: true,
  };

  $rows: ClassSession[] = [];

  constructor(
    private classService: ClassService,
    private router: Router,
    public snackBar: MatSnackBar,
    public activeRoute: ActivatedRoute,
    private toaster: ToastrService,
    private sessionStore: SessionStore
  ) {}

  ngOnInit(): void {
    this.loadClasses();
  }

  private loadClasses() {
    this.classService.studentByclass().subscribe((x) => {
      this.$rows = x.selectedClasses;
      this.Titles = x.availableClasses;
    });
  }

  Save() {
    if (!this.selectedTitle) {
      return;
    }
    // this.classService.SaveClass(this.selectedTitle).subscribe((response) => {
    //   this.loadClasses();
    //   this.snackBar.open('Student Assign Class Saved Successfully');
    // });
  }
  addIntoClass() {
    if (this.selectedTitle) {
      this.classService.addStudentInClass(this.selectedTitle).subscribe({
        next: () => {
          this.toaster.success('Class added successfully!!');
          this.loadClasses();
        },
        error: (err) => {
          let msg = Object.values(err.error).join(',');
          this.toaster.error(msg);
        },
      });
    }
  }

  loadReport(): ($event: Event, row: any) => void {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      this.router.navigate(['../report', row.data?.classId, 'list'], {
        relativeTo: this.activeRoute,
      });
    };
  }

  setAsDefault(): ($event: Event, row: any) => void {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      this.classService.setAsDefault(row.data!).subscribe((data) => {
        this.sessionStore.SetDefaultClass(data);
        this.loadClasses();
        this.snackBar.open('Class added succesfully');
        window.location.reload();
      });
    };
  }
}
