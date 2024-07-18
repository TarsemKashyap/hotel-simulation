import { Component } from '@angular/core';
import { AccountService } from 'src/app/public/account';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../student.service';
import { RolePagesDtl, StudentRoles } from 'src/app/shared/class/model/Roles';
import { SessionStore } from 'src/app/store';
import { ClassService, ClassSession } from 'src/app/shared/class';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
})
export class StudentDashboard {
  studentId: string = '';
  studentRoleList: StudentRoles[] = [];
  studentRolePageList: RolePagesDtl[] = [];
  defaultClass: ClassSession | undefined;
  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private studentService: StudentService,
    private sessionStore: SessionStore,
    private classService: ClassService,
    private sanckBar:MatSnackBar
  ) {}

  ngOnInit(): void {
    this.studentId = this.activeRoute.snapshot.params['id'];
    this.getDefaultClass();
    this.studentRolesList();
  }

  getDefaultClass() {
    this.classService.getStudentDefaultClass().subscribe({
      next: (data) => {
        this.defaultClass = data;
      },
      error: (err) => {
        this.sanckBar.open('No default class found for Student');
        console.log(err);
      },
    });
  }

  logout() {
    this.accountService.clearSession();
    this.router.navigate([`login`]);
  }

  private studentRolesList() {
    this.studentService.StudentRoleslist().subscribe((data) => {
      this.studentRoleList = data;
      this.sessionStore.SetStudentRole(this.studentRoleList);

      this.studentRolePageList = this.sessionStore.GetStudentRole();
    });
  }

  navigateToReport() {
    if (this.defaultClass) {
      this.router.navigate(['./report', this.defaultClass.classId, 'list'], {
        relativeTo: this.activeRoute,
      });
    }
  }

  openLink(studentRolePage: RolePagesDtl) {
    this.sessionStore.SetCurrentRole(studentRolePage.roleName);
    this.router.navigate(['./student', studentRolePage.childPageLink]);
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
