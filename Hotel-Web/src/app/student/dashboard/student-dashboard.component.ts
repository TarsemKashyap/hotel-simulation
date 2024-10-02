import { Component } from '@angular/core';
import { AccountService } from 'src/app/public/account';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../student.service';
import { RolePagesDtl, StudentRoles } from 'src/app/shared/class/model/Roles';
import { SessionStore } from 'src/app/store';
import { ClassService, ClassSession } from 'src/app/shared/class';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToastrService } from 'ngx-toastr';

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
  roleLabel:string='';
  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private studentService: StudentService,
    private sessionStore: SessionStore,
    private classService: ClassService,
    private sanckBar: ToastrService
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
        let mesg = Object.values(err.error).join(',');
        this.sanckBar.error(mesg);
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
      this.roleLabel = this.studentRoleList.map((x) => x.roleName).join(', ');

      this.sessionStore.SetStudentRole(data);
      this.studentRolePageList = this.sessionStore.GetStudentRoutes();
      console.log('studentRolePageList', {
        studentRolePageList: this.studentRolePageList,
      });
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
    const url=['./student', studentRolePage.childPageLink];
    this.router.navigate(url);
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
