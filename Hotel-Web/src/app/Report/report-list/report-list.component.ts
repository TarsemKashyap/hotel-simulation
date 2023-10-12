import { Component } from '@angular/core';
import { AccountService, AppRoles } from 'src/app/public/account';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css'],
})
export class ReportListComponent {
  isInstructor: boolean=false;
  constructor(private accountService: AccountService) {}
  ngOnInit() {
    this.isInstructor = this.accountService.userHasRole(AppRoles.Instructor);
  }
}
