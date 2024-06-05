import { Component } from '@angular/core';
import { AccountService, AppRoles } from 'src/app/public/account';
import { IHyperLinks } from './IHyperLinks.model';
import { ActivatedRoute } from '@angular/router';
import { ClassService, ClassSession } from 'src/app/shared/class';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css'],
})
export class ReportListComponent {
  isInstructorOrAdmin: boolean = false;
  activeLinks: IHyperLinks[] = [];
  studentLink: IHyperLinks[] = [
    {
      text: 'Objectives Report',
      link: 'objective-report',
    },
    {
      text: 'Performance Report',
      link: 'performance-report',
      enable: () =>
        !this.isInstructorOrAdmin && this.classInfo.currentQuater > 1,
    },
    {
      text: 'Performance Report',
      link: 'inst-performance-report',
      enable: () =>
        this.isInstructorOrAdmin && this.classInfo.currentQuater > 1,
    },
    {
      text: 'Instructor Hotels Summery Report',
      link: 'inst-hotels-summery-report',
      enable: () =>
        this.isInstructorOrAdmin && this.classInfo.currentQuater > 1,
    },
    {
      text: 'Income Report',
      link: 'income-report',
    },
    {
      text: 'Balance Report',
      link: 'balance-report',
      enable: () => this.classInfo.currentQuater > 0,

    },
    {
      text: 'Cash Flow Report',
      link: 'cashflow-report',
      enable: () => this.classInfo.currentQuater > 2,
    },
    {
      text: 'Occupancy Report',
      link: 'occupancy-report',
    },
    {
      text: 'Avgerage Daily Rate Report',
      link: 'avg-daily-rate-report',
    },
    {
      text: 'Rev Par Go par Report',
      link: 'rev-par-gopar-report',
    },
    {
      text: 'RoomRate Report',
      link: 'room-rate-report',
    },
    {
      text: 'Market Share Revenue Report',
      link: 'market-share-revenue-report',
    },
    {
      text: 'Market Share Room Sold Report',
      link: 'market-share-roomsold-report',
    },
    {
      text: 'Market Share Position Alone Report',
      link: 'market-share-position-alone-report',
    },
    {
      text: 'Attribute Amentities Report',
      link: 'attribute-amentities-report',
    },
    {
      text: 'Market Expenditure Report',
      link: 'market-expenditure-report',
    },
    {
      text: 'Position Map Report',
      link: 'position-map-report',
    },
    {
      text: 'Quality Rating Report',
      link: 'quality-rating-report',
    },
    {
      text: 'Demand Report',
      link: 'demand-report',
    },
  ];
  classInfo: ClassSession;
  constructor(
    private accountService: AccountService,
    public activeRoute: ActivatedRoute,
    public classService: ClassService
  ) {}

  ngOnInit() {
    this.isInstructorOrAdmin = this.accountService.userHasAnyRole([
      AppRoles.Instructor,
      AppRoles.Admin,
    ]);
    this.classService
      .getClass(this.activeRoute.snapshot.params['id'])
      .subscribe((x) => {
        this.classInfo = x;
        this.links();
      });
  }

  private links() {
    this.activeLinks = this.studentLink.filter((x) => {
      if (!x.enable) {
        return this.classInfo.currentQuater > 1;
      }
      return x.enable();
    });
  }
}
