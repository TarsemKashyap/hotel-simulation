import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class DashboardComponent {
  constructor(
    private router: Router,
    private sessionStore: SessionStore
  ) { }

  logout() {
    this.sessionStore.RemoveAccessToken();
    this.router.navigate([`login`]);
  }

  changePassword() {
    this.router.navigate([`change-password`]);
  }

}
