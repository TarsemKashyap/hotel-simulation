import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { InstructorService } from '../instructor.service';
import { InstructorDto } from '../instructor.model';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-instructor-list',
  templateUrl: './instructor-list.component.html',
  styleUrls: ['./instructor-list.component.css'],
})
export class InstructorListComponent {
  accountList: InstructorDto[] = [];
  dataSource = new MatTableDataSource<InstructorDto>();

  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'email',
    'institute',
    'action',
  ];
  constructor(
    private instructorService: InstructorService,
    private router: Router,
    public dialog: MatDialog,
    public snackBar: MatSnackBar
  ) {}
  @ViewChild(MatSort)
  sort!: MatSort;

  ngOnInit(): void {
    this.instructorService.instructorList().subscribe((data) => {
      this.accountList = data;
      this.dataSource.data = data;
    });
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  editClick(row: InstructorDto) {
    console.log(row);
    this.router.navigate([`instructor/edit/${row.userId}`]);
  }

  deleteRow(row: InstructorDto) {
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
        this.instructorService.deleteUser(row.userId).subscribe({
          next: () => {
            window.location.reload();
          },
          error: (err) => {
            this.snackBar.open('Error while deleting user.');
          },
        });
      }
    });
  }
}
