import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClassService } from '../class.service';
import { StudentList } from '../model/studentList.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup } from '@angular/forms';
import { StudentGroupList, StudentRoles } from '../model/Roles';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-student-roles-edit',
  templateUrl: './student-roles-edit.component.html',
  styleUrls: ['./student-roles-edit.component.css']
})
export class StudentRolesEditComponent {
  studentId: string | undefined;
  data: StudentList| undefined;
  roles: StudentRoles[] = [];
  myForm!: FormGroup;
  groups: StudentGroupList[] = [];


  constructor(
    private route: ActivatedRoute,
    private classService: ClassService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<StudentRolesEditComponent>,
    @Inject(MAT_DIALOG_DATA) public row: any
  ) {
    this.myForm = this.formBuilder.group({
      selectedRoles: [[]], 
      selectedGroup: ['']
    });
  }


  ngOnInit(): void {
    this.studentId = this.route.snapshot.params['id'];
    this.studentRoles();
    this.studentGroups();
  }

  private studentRoles() {
    this.classService.Roleslist().subscribe((data) => {
      this.roles = data;
      console.log("groups",this.roles)
    });
  }


  private studentGroups() {
    this.classService.Grouplist().subscribe((data) => {
        this.groups = data;
        console.log("groups",this.groups)
    });
  }

  Save() {
    
  }
}
