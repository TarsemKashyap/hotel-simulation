import { Component } from '@angular/core';
import { ActivatedRoute, DefaultUrlSerializer, Router } from '@angular/router';
import { AccountService, AppRoles } from 'src/app/public/account';
import { IHyperLinks } from 'src/app/Report/report-list/IHyperLinks.model';
import { StudentRoles as StudentRoles } from 'src/app/shared/class/model/StudentRoles';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-decision',
  templateUrl: './decision.component.html',
  styleUrls: ['./decision.component.css'],
})
export class DecisionComponent {
  studentRoles: number[];
  activeLinks: IHyperLinks[] = [];
  private decisionLinks: IHyperLinks[] = [
    {
      link: '../room',
      text: 'Room Allocation Decisions',
      enable: () =>
        this.hasAnyRole([
          StudentRoles.GeneralManager,
          StudentRoles.RetailOperationsManager,
        ]),
    },
    {
      link: '../attribute',
      text: 'Hotel Attributes and Amenities Decisions',
      enable: () =>
        this.hasAnyRole([
          StudentRoles.GeneralManager,
          StudentRoles.RoomManager,
          StudentRoles.FBManager,
          StudentRoles.RetailOperationsManager,
        ]),
    },
    {
      link: '../price',
      text: 'Room Rate Decisions',
      enable: () =>
        this.hasAnyRole([
          StudentRoles.GeneralManager,
          StudentRoles.RevenueManager,
        ]),
    },
    {
      link: '../marketing',
      text: 'Marketing Decisions',
      enable: () =>
        this.hasAnyRole([
          StudentRoles.GeneralManager,
          StudentRoles.MarketingManager,
        ]),
    },
  ];
  constructor(
    private sessionStore: SessionStore,
    private accountService: AccountService,
  ) {
    this.studentRoles = this.sessionStore.GetRoleids();
  }

  private hasAnyRole(roles: StudentRoles[]) {
    return (
      this.accountService.userHasRole(AppRoles.Instructor) ||
      roles.some((x) => this.studentRoles.includes(x))
    );
  }

  ngOnInit(): void {
    this.activeLinks = this.decisionLinks.filter((x) => {
      if (!x.enable) return true;
      return x.enable();
    });
  }
}
