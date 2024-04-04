import { Component } from '@angular/core';
import { AccountService, AppRoles } from 'src/app/public/account';
import { IHyperLinks } from './IHyperLinks.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css'],
})
export class ReportListComponent {
  isInstructorOrAdmin: boolean = false;
  activeLinks: IHyperLinks[] = [];
  private studentLink: IHyperLinks[] = [
    {
      text: 'Objectives Report',
      link: 'objective-report',
    },
    {
      text: 'Performance Report',
      link: 'performance-report',
      enable: () => !this.isInstructorOrAdmin,
    },
    {
      text: 'Performance Report',
      link: 'inst-performance-report',
      enable: () => this.isInstructorOrAdmin,
    },
    {
      text: 'Instructor Hotels Summery Report',
      link: 'inst-hotels-summery-report',
      enable: () => this.isInstructorOrAdmin,
    },
    {
      text: 'Income Report',
      link: 'income-report',
    },
    {
      text: 'Balance Report',
      link: 'balance-report',
    },
    {
      text: 'Cash Flow Report',
      link: 'cashflow-report',
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
  constructor(private accountService: AccountService, public activeRoute: ActivatedRoute) {}

  ngOnInit() {
    this.isInstructorOrAdmin = this.accountService.userHasAnyRole([
      AppRoles.Instructor,
      AppRoles.Admin,
    ]);
    const classId = this.activeRoute.snapshot.params['id'];

    
    this.activeLinks = this.studentLink.filter((x) => {
      if (!x.enable) return true;
      return x.enable();
    });
  }
}
