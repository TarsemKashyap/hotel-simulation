import { Component } from '@angular/core';
import { AccountService } from 'src/app/public/account';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../student.service';
import { RolePagesDtl, StudentRoles } from 'src/app/shared/class/model/Roles';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
})
export class StudentDashboard {
  studentId: string = '';
  studentRoleList: StudentRoles[] = [];
  studentRolePageList: RolePagesDtl[] = [];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private studentService: StudentService,
    private sessionStore: SessionStore
  ) {}

  ngOnInit(): void {
    this.studentId = this.route.snapshot.params['id'];
    this.studentRolesList();
  }

  logout() {
    this.accountService.clearSession();
    this.router.navigate([`login`]);
  }

  // private studentRolesList() {
  //   // this.studentId = '01b96b31-649a-4b87-a4a4-4c63f6c4d636';
  //   this.studentService
  //     .StudentRoleslist({ studentId: this.studentId })
  //     .subscribe((data) => {
  //       this.studentRoleList = data;
  //       this.sessionStore.SetStudentRole(this.studentRoleList);
  //       //var selectedRolesArr = JSON.parse(localStorage.getItem(studentRole) || '[]');
  //       this.studentRolePageList = JSON.parse(
  //         this.sessionStore.GetStudentRole()
  //       );
  //       console.log(
  //         this.studentRolePageList,
  //         this.sessionStore.GetStudentRole()
  //       );
  //     });
  // }

  private studentRolesList() {
    this.studentService.StudentRoleslist().subscribe((data) => {
      this.studentRoleList = data;
      this.sessionStore.SetStudentRole(this.studentRoleList);

      this.studentRolePageList = JSON.parse(this.sessionStore.GetStudentRole());
      this.studentRolePageList.unshift(
        {
          pageKey: 'ChangePwd',
          pageName: 'Change password',
          roleName: '',
          childPageLink: 'change-password',
        }
      );
    });
  }

  openLink(studentRolePage: RolePagesDtl) {
    this.sessionStore.SetCurrentRole(studentRolePage.roleName);
    this.router.navigate(['./student',studentRolePage.childPageLink])
    // this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    //   this.router.navigate([studentRolePage.childPageLink]);
    // });
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
