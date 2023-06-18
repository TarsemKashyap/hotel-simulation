import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account';

@Component({
  selector: 'header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.css'],
})
export class HeaderMenuComponent implements OnInit {
  isLoggedIn: boolean = false;
  constructor(private accountService: AccountService) {}
  ngOnInit(): void {
    this.isLoggedIn = this.accountService.isLoggedIn();
  }

  redirectToDashboard() {
    if (this.isLoggedIn) {
      this.accountService.redirectToDashboard();
    }
  }
}
