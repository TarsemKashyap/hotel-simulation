import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { StudentList } from '../model/studentList.model';
import { ClassService } from '../class.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentRolesEditComponent } from '../student-roles-edit/student-roles-edit.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent {
  accountList: StudentList[] = [];
  classId: number | undefined;
  dataSource = new MatTableDataSource<StudentList>();

  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'email',
    'institute',
    'classCode',
    'title',
    'action'
  ];
  constructor(
    private studentClassMappingService: ClassService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.classId = this.route.snapshot.params['id'];
    this.studentClassMappingService.studentClassMappingList(this.classId).subscribe((data) => {
      this.accountList = data;
      this.dataSource.data = data;
    });
  }

  editClick(row: StudentList) {
    const dialogRef = this.dialog.open(StudentRolesEditComponent, {
      minWidth: '300px',
      minHeight: '100px',
      data: Object.assign({},row,{classId:this.classId})
    });
    
  }
}
