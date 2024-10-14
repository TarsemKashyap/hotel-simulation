import { Component, Injector } from '@angular/core';
import { ActivatedRoute, DefaultUrlSerializer, Router } from '@angular/router';
import { AccountService, AppRoles } from 'src/app/public/account';
import { IHyperLinks } from 'src/app/Report/report-list/IHyperLinks.model';
import { StudentRoles } from 'src/app/shared/class/model/StudentRoles';
import { SessionStore } from 'src/app/store';
import { DecisionService } from './decision.service';

export abstract class BaseDecision {
  private activatedRoute: ActivatedRoute;
  private decisionService: DecisionService;
  private router1: Router;

  constructor(injector: Injector) {
    this.activatedRoute = injector.get(ActivatedRoute);
    this.decisionService = injector.get(DecisionService);
    this.router1 = injector.get(Router);
  }

  public get classId(): string {
    return this.activatedRoute.snapshot.paramMap.get('id')!;
  }

  public async getActiveClass() {
    if (this.decisionService.IsStudent) {
      return this.decisionService.GetClass();
    }
    return await this.decisionService.GetClassById(this.classId);
  }
}

@Component({
  selector: 'app-decision',
  templateUrl: './decision.component.html',
  styleUrls: ['./decision.component.css'],
})
export class DecisionComponent extends BaseDecision {
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
    injector: Injector
  ) {
    super(injector);
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
