  import { Component, Inject } from '@angular/core';
  import { ActivatedRoute } from '@angular/router';
  import { ClassService } from '../class.service';
  import { StudentList } from '../model/studentList.model';
  import { MatSnackBar } from '@angular/material/snack-bar';
  import { FormBuilder, FormGroup } from '@angular/forms';
  import { StudentGroupList, StudentRoleGroupRequest, StudentRoles } from '../model/Roles';
  import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
  import { StudentRoleGroupAssign } from '../model/StudentRoles';
import { ClassGroup } from '../model/classSession.model';

  @Component({
    selector: 'app-student-roles-edit',
    templateUrl: './student-roles-edit.component.html',
    styleUrls: ['./student-roles-edit.component.css']
  })
  export class StudentRolesEditComponent {
    studentId: string | undefined;
    data: StudentList| undefined;
    roles: StudentRoleGroupRequest[] = [];
    selectedRoles: StudentRoles[] = [];;
    selectedGroup: any;  
    myForm!: FormGroup;
    groups: ClassGroup[] = [];
    StudentAssignRoles : StudentRoles[] = [];


    constructor(
      private route: ActivatedRoute,
      private classService: ClassService,
      private formBuilder: FormBuilder,
      private _snackBar: MatSnackBar,
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
    }

    private studentRoles() {
      this.classService.Roleslist({studentId:this.row.studentId,classId:this.row.classId}).subscribe((data) => {
        this.selectedRoles = data.selectedRoles;
        this.selectedGroup = this.selectedGroup;
        this.groups = data.classGroups;
        this.StudentAssignRoles = data.studentRole;
      });
    }
    
    Save() {
    const studentId = this.row.studentId;
    const groupId = this.selectedGroup.groupId;
    const roleIds = this.selectedRoles.map((role: { id: number; }) => role.id);
    const studentAssignRoles: StudentRoleGroupAssign = {
      studentId: studentId,
      GroupId: groupId,
      Roles: roleIds
    };
      this.classService.AddRoles(studentAssignRoles).subscribe(response => {
        this._snackBar.open('Student Role Assign successfully');
        this.dialogRef.close();
      });
    }

    Cancel() {
      this.dialogRef.close();
    }

    compareFn(option: string, selectedRoles: any): boolean {
      console.log(selectedRoles.roleName)
      return option === selectedRoles.roleName;
    }
    
  }
