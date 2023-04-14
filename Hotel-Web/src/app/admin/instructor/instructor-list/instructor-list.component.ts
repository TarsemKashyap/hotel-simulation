import { Component } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { InstructorService } from '../instructor.service';
import { InstructorDto } from '../instructor.model';
import { MatRow } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-instructor-list',
  templateUrl: './instructor-list.component.html',
  styleUrls: ['./instructor-list.component.css'],
})
export class InstructorListComponent {
  accountList: InstructorDto[] = [];
  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'email',
    'institute',
    'action',
  ];
  constructor(
    private instructorService: InstructorService,
    private router: Router
  ) {}
  columnDefs: ColDef[] = [
    { field: 'firstName' },
    { field: 'lastName' },
    { field: 'email' },
    { field: 'institute' },
    { field: 'action' },
  ];

  ngOnInit(): void {
    this.instructorService.instructorList().subscribe((data) => {
      this.accountList = data;
    });
  }

  get gridOptions() {
    const gridOptions: GridOptions = {
      columnDefs: this.columnDefs,
      domLayout: 'autoHeight',
      rowData: this.accountList,
    };
    return gridOptions;
  }

  editClick(row: InstructorDto) {
    console.log(row);
    this.router.navigate([`instructor/edit/${row.userId}`]);
  }
}
