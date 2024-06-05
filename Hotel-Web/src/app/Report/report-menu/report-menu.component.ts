import { Component, Injector, ViewChild } from '@angular/core';
import { ReportListComponent } from '../report-list/report-list.component';
import { AccountService } from 'src/app/public/account';
import { ActivatedRoute } from '@angular/router';
import { MatMenuTrigger } from '@angular/material/menu';
import { ClassService } from 'src/app/shared/class';

@Component({
  selector: 'app-report-menu',
  templateUrl: './report-menu.component.html',
  styleUrls: ['./report-menu.component.css'],
})
export class ReportMenuComponent extends ReportListComponent {
  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;
  constructor(
    accountService: AccountService,
    activeRoute: ActivatedRoute,
    classService: ClassService
  ) {
    super(accountService, activeRoute, classService);
  }

  openMyMenu() {
    this.trigger.openMenu();
  }
  closeMenu() {
    this.trigger.closeMenu();
  }
}
