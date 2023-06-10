import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { StudentList } from '../model/studentList.model';
import { ClassService } from '../class.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentRolesEditComponent } from '../student-roles-edit/student-roles-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { GridActionComponent } from '../grid-action/grid-action.component';
import { GridActionParmas, RowAction } from '../grid-action/grid-action.model';
import { ColDef, IRowNode } from 'ag-grid-community';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent {
  classId: number | undefined;
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
            placeHolder: 'visibility',
            mode: 'icon',
            cssClass: 'hover:text-primary',
            onClick: this.onOverviewClick(),
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
    menuTabs: ['filterMenuTab'],
    sortable: true,
  };

  $rows: StudentList[] = [];

  constructor(
    private classService: ClassService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    public snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.classId = this.route.snapshot.params['id'];
    this.classService.studentClassMappingList(this.classId).subscribe((x) => {
      debugger
      this.$rows = x;
    });
  }

  onOverviewClick() {
    return ($event: Event, row: IRowNode<StudentList>) => {
      const dialogRef = this.dialog.open(StudentRolesEditComponent, {
             minWidth: '300px',
             minHeight: '100px',
             data: Object.assign({},row.data,{classId:this.classId})
           });
    };
  }

  classEdit() {
      this.router.navigate([`class/edit/${this.classId}`]);
    }
    
  }