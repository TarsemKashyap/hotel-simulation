import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StudentList } from '../../class/model/studentList.model';
import { InstructorDecisionManager } from '../InstructorDecisionManager';

@Component({
  selector: 'app-student-decisions',
  templateUrl: './student-decisions.component.html',
  styleUrls: ['./student-decisions.component.css'],
  providers:[InstructorDecisionManager]
})
export class StudentDecisionsComponent {
  constructor(
    private dialogRef: MatDialogRef<StudentDecisionsComponent>,
    @Inject(MAT_DIALOG_DATA) public row: StudentList
  ) {}
}
