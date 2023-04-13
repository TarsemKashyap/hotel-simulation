import { Component } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import { InstructorService } from '../instructor.service';
import { InstructorDto } from '../instructor.model';

@Component({
  selector: 'app-instructor-list',
  templateUrl: './instructor-list.component.html',
  styleUrls: ['./instructor-list.component.css'],
})
export class InstructorListComponent {
  accountList: InstructorDto[] = [];
  constructor(private instructorService: InstructorService) {}
  columnDefs: ColDef[] = [
    { field: 'firstName' },
    { field: 'lastName' },
    { field: 'email' },
    { field: 'institute' },
  ];

  ngOnInit(): void {
    this.instructorService.instructorList().subscribe((data) => {
      this.accountList = data;
    });
  }
}
