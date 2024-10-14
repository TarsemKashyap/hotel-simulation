import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-student-decisions',
  templateUrl: './student-decisions.component.html',
  styleUrls: ['./student-decisions.component.css'],
})
export class StudentDecisionsComponent {
  constructor(
    private dialogRef: MatDialogRef<StudentDecisionsComponent>,
    @Inject(MAT_DIALOG_DATA) public row: any
  ) {}
}
