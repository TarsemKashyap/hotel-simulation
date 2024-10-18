import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StudentList } from '../../class/model/studentList.model';
import { InstructorDecisionManager } from '../InstructorDecisionManager';
import { StudentRoles } from '../../class';

@Component({
  selector: 'app-student-decisions',
  templateUrl: './student-decisions.component.html',
  styleUrls: ['./student-decisions.component.css'],
  providers: [InstructorDecisionManager],
})
export class StudentDecisionsComponent {
  showRoom: boolean = false;
  showAttribute: boolean = false;
  showRate: boolean = false;
  showmkt: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<StudentDecisionsComponent>,
    @Inject(MAT_DIALOG_DATA) public row: StudentList
  ) {
    this.showRoom = this.hasRoles([
      StudentRoles.GeneralManager,
      StudentRoles.RetailOperationsManager,
    ]);
    this.showAttribute = this.hasRoles([
      StudentRoles.GeneralManager,
      StudentRoles.RoomManager,
      StudentRoles.FBManager,
      StudentRoles.RetailOperationsManager,
    ]);

    this.showRate = this.hasRoles([
      StudentRoles.GeneralManager,
      StudentRoles.RevenueManager,
    ]);
    this.showmkt = this.hasRoles([
      StudentRoles.GeneralManager,
      StudentRoles.MarketingManager,
    ]);
  }

  private hasRoles(studentRole: StudentRoles[]) {
    return this.row.roles.some((x) => studentRole.includes(x.id));
  }
}
