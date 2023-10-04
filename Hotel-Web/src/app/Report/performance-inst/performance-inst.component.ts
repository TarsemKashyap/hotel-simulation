import { Component } from '@angular/core';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportParams } from '../model/ReportParams.model';
import { PerformanceInstReport, PerformanceResponse } from '../model/PerformanceResponse.model';

@Component({
  selector: 'app-performance-inst',
  templateUrl: './performance-inst.component.html',
  styleUrls: ['./performance-inst.component.css'],
})
export class PerformanceInstComponent {
  selectedMonth: MonthDto = {} as MonthDto;
  MonthList: MonthDto[] = [];
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;
  classId: number | undefined;
  reportParam: ReportParams = {} as ReportParams;
  reportDto: PerformanceInstReport = {} as PerformanceInstReport;

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
    this.loadMonths();
  }

  loadPerformanceDetails() {
    this.reportParam.ClassId = this.classId!;
    this.reportParam.GroupId = this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = 0;
    this.reportService
      .performanceInstReport(this.reportParam)
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
