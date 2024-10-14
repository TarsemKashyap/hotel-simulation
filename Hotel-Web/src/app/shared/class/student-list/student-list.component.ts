import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ClassOverview, StudentList } from '../model/studentList.model';
import { ClassService } from '../class.service';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { StudentRolesEditComponent } from '../student-roles-edit/student-roles-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { GridActionComponent } from '../grid-action/grid-action.component';
import { GridActionParmas, RowAction } from '../grid-action/grid-action.model';
import { ColDef, IRowNode } from 'ag-grid-community';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';
import { ClassInformation, ClassSession } from '../model/classSession.model';
import { ClassModule } from '../class.module';
import { StudentDecisionsComponent } from '../../decisions/student-decisions/student-decisions.component';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css'],
  providers: [DatePipe],
})
export class StudentListComponent {
  classId: number | undefined;
  classSession: ClassInformation | undefined;
  columnDefs: ColDef[] = [
    {
      field: 'firstName',
    },
    { field: 'lastName' },
    { field: 'email' },
    { field: 'institute' },
    { field: 'groupName' },
    {
      field: 'action',
      cellRenderer: GridActionComponent,
      cellRendererParams: {
        actions: [
          {
            placeHolder: 'group',
            mode: 'icon',
            tooltip: 'Manage groups',
            cssClass: 'hover:text-primary',
            onClick: this.onOverviewClick(),
            hide: () => false,
          },
          {
            placeHolder: 'visibility',
            mode: 'icon',
            tooltip: 'View Decisions',
            cssClass: 'hover:text-primary',
            onClick: this.onDecisionClick(),
            hide: () => false,
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

  $rows: StudentList[] = [];

  constructor(
    private classService: ClassService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.classId = this.activatedRoute.snapshot.params['id'];
    this.loadStudentList();
  }

  private loadStudentList() {
    this.classService.studentClassMappingList(this.classId!).subscribe((x) => {
      this.$rows = x.studentClassMappingDto;
      this.classSession = x.classSessionDto;
    });
  }

  onDecisionClick() {
    return ($event: Event, row: IRowNode<StudentList>) => {
      const dialogRef = this.dialog.open(StudentDecisionsComponent, {
        width: '90%',
        height: '90%',
        data: Object.assign({}, row.data, { classId: this.classId }),
      });

      dialogRef.beforeClosed().subscribe((x) => {
        if (x) {
          this.loadStudentList();
        }
      });
    };
  }

  onOverviewClick() {
    return ($event: Event, row: IRowNode<StudentList>) => {
      const dialogRef = this.dialog.open(StudentRolesEditComponent, {
        minWidth: '300px',
        minHeight: '100px',
        data: Object.assign({}, row.data, { classId: this.classId }),
      });

      dialogRef.beforeClosed().subscribe((x) => {
        if (x) {
          this.loadStudentList();
        }
      });
    };
  }
}
