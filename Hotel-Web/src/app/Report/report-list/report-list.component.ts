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
      text: 'Income Statement',
      link: 'income-report',
    },
    {
      text: 'Balance Sheet',
      link: 'balance-report',
      enable: () => this.classInfo.currentQuater > 0,

    },
    {
      text: 'Cash Flow Statement',
      link: 'cashflow-report',
      enable: () => this.classInfo.currentQuater > 2,
    },
    {
      text: 'Occupancy Report',
      link: 'occupancy-report',
    },
    {
      text: 'Avgerage Daily Rate (ADR) Report',
      link: 'avg-daily-rate-report',
    },
    {
      text: 'REVPAR, TOTAL REVPAR and GOPAR Reports',
      link: 'rev-par-gopar-report',
    },
    {
      text: 'Room Rates and Cost of Distribution Channel Reports',
      link: 'room-rate-report',
    },
    {
      text: 'Market Share based on Revenues Reports',
      link: 'market-share-revenue-report',
    },
    {
      text: 'Market Share based on Number of Rooms Sold Reports',
      link: 'market-share-roomsold-report',
    },
    {
      text: 'Actual Market Share and Market Share by Positioning Alone Reports',
      link: 'market-share-position-alone-report',
    },
    {
      text: 'Hotel Attributes and Amenities Report',
      link: 'attribute-amentities-report',
    },
    {
      text: 'Marketing Expenditure Reports',
      link: 'market-expenditure-report',
    },
    {
      text: 'Positioning Maps',
      link: 'position-map-report',
    },
    {
      text: 'Quality Perceptions and Ratings Reports',
      link: 'quality-rating-report',
    },
    {
      text: 'Demand Reports',
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
