import { Component, OnInit } from '@angular/core';
import { ClassService } from '../class.service';
import { ColDef, IRowNode, RowNode } from 'ag-grid-community';
import { ClassSession } from '../model/classSession.model';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { GridActionParmas, RowAction } from '../grid-action/grid-action.model';
import { GridActionComponent } from '../grid-action/grid-action.component';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from '../../dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Utility } from '../../utility';

@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css'],
})
export class ClassListComponent implements OnInit {
  private datePipe = new DatePipe('en-US');

  columnDefs: ColDef[] = [
    {
      field: 'code',
      tooltipValueGetter: () => 'Click to copy code',
      onCellClicked: (event) => {
        Utility.copyToClipboard(event.value);
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
      field: 'createdOn',
      cellRenderer: (params: { value: string | number | Date }) =>
        this.datePipe.transform(params.value, 'dd-MM-yyyy'),
    },
    {
      field: 'action',
      cellRenderer: GridActionComponent,
      cellRendererParams: {
        actions: [
          {
            placeHolder: 'assignment',
            mode: 'icon',
            onClick: this.navigateToReport(),
            hide: () => false,
            tooltip: 'View class Reports',
          },
          {
            placeHolder: 'visibility',
            mode: 'icon',
            onClick: this.onOverviewClick(),
            hide: () => false,
            tooltip: 'Class overview',
          },
          {
            placeHolder: 'delete',
            mode: 'icon',
            cssClass: 'text-red-500  hover:text-primary',
            onClick: this.onDeleteback(),
            hide: () => false,
            tooltip: 'Delete class',
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
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    public activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.classService.list().subscribe((x) => {
      this.$rows = x;
    });
  }
  onEditCallback() {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      console.log('Edit Class', row);
      this.router.navigate(['..', row.data?.classId, 'edit'], {
        relativeTo: this.activeRoute,
      });
    };
  }

  onOverviewClick() {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      this.router.navigate(['..', row.data?.classId, 'student-list'], {
        relativeTo: this.activeRoute,
      });
    };
  }

  navigateToReport() {
    return ($event: Event, row: IRowNode<ClassSession>) => {
      this.router.navigate(['../../report', row.data?.classId, 'list'], {
        relativeTo: this.activeRoute,
      });
    };
  }

  onDeleteback() {
    return ($event: Event, row: IRowNode<ClassSession>) => {
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
          this.classService.deleteClass(row.data!.classId!).subscribe({
            next: () => {
              window.location.reload();
            },
            error: (err) => {
              this.snackBar.open('Error while deleting class.');
            },
          });
        }
      });
    };
  }
}
