import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/public/account';

@Component({
  selector: 'instructor-dashboard',
  templateUrl: './instructor-dashboard.component.html',
  styleUrls: ['./instructor-dashboard.component.css'],
})
export class InstructorDashboard implements OnInit {
  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {}
  ngOnInit(): void {
  
  }

  logout() {
    this.accountService.clearSession();
    //this.router.navigate([`login`]);
  }
}
