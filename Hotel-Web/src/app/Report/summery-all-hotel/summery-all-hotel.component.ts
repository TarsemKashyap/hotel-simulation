import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { PerformanceInstReport } from '../model/PerformanceResponse.model';
import { ReportParams } from '../model/ReportParams.model';
import { ReportService } from '../report.service';
import { SummeryAllHotelsReport } from '../model/SummeryAllHotelsReport';

@Component({
  selector: 'app-summery-all-hotel',
  templateUrl: './summery-all-hotel.component.html',
  styleUrls: ['./summery-all-hotel.component.css'],
})
export class SummeryAllHotelComponent {
  selectedMonth: MonthDto = {} as MonthDto;
  MonthList: MonthDto[] = [];
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;
  classId: number | undefined;
  reportParam: ReportParams = {} as ReportParams;
  reportDto: SummeryAllHotelsReport[] = []

  constructor(
    private reportService: ReportService,
    private router: Router,
    public activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.classId = this.activeRoute.snapshot.params['id'];
    this.loadMonths();
  }

  onOptionChange() {
   this.loadPerformanceDetails();
  }

  loadPerformanceDetails() {
    this.reportParam.ClassId = this.classId!;
    this.reportParam.GroupId = this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = this.selectedHotel?.serial!;
    this.reportService
      .summeryAllHotels(this.reportParam)
      .subscribe((reportData) => {
        this.reportDto = reportData;
      });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;
      this.selectedMonth = this.MonthList.at(this.MonthList.length - 1)!;
      this.loadGroups();
    });
  }

  private loadGroups() {
    this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
      this.groups = groups;
      this.selectedHotel = this.groups.at(0);
      this.loadPerformanceDetails();
    });
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
  numberWithCommas(x: any) {
    return this.reportService.numberWithCommas(x);
  }
}
