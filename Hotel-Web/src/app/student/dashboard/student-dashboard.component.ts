import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/public/account';

@Component({
  selector: 'student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
})
export class StudentDashboard {
  constructor(private router: Router, private accountService: AccountService) {}

  logout() {
    this.accountService.clearSession();
    this.router.navigate([`login`]);
  }
}
