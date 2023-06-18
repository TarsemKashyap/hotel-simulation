import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/public/account';

@Component({
  selector: 'instructor-dashboard',
  templateUrl: './instructor-dashboard.component.html',
  styleUrls: ['./instructor-dashboard.component.css']
})
export class InstructorDashboard {
  constructor(private router: Router, private accountService: AccountService) {}

  logout() {
    this.accountService.clearSession();
    this.router.navigate([`login`]);
  }
}
