import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { AttributeAmentitesReportResponse } from '../model/AttributeAmentitesResponse.model';
import Chart from 'chart.js/auto';
import { AttributeAmentitesReportAttribute } from '../model/ReportCommon.model';
@Component({
  selector: 'app-attribute-amentities',
  templateUrl: './attribute-amentities.component.html',
  styleUrls: ['./attribute-amentities.component.css'],
})
export class AttributeAmentitiesComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  attributeAmentitesReportResponse: AttributeAmentitesReportResponse =
    {} as AttributeAmentitesReportResponse;
  public chart: any;
  ChartData: AttributeAmentitesReportAttribute[] = [];

  Xaxis: any[] = [];
  YaxisMktShrPos: any[] = [];
  YaxisActualMktShr: any[] = [];
  constructor(
    private reportService: ReportService,
    private router: Router,
    public snackBar: MatSnackBar,
    public activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.classId = this.activeRoute.snapshot.params['id'];
    this.loadMonths();
  }

  onOptionChange() {
    this.loadReportDetails();
  }

  loadReportDetails() {
    this.reportParam.ClassId = this.classId!;
    this.reportParam.GroupId = this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = parseInt(this.selectedMonth.sequence!);
    this.reportService
      .marketShareAttributeAmentitesReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.attributeAmentitesReportResponse = reportData;
      });
  }

  private loadGroups() {
    this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
      this.groups = groups;
      this.selectedHotel = this.groups.at(0);
      this.loadReportDetails();
    });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;
      this.selectedMonth = this.MonthList.at(this.MonthList.length - 1)!;
      this.loadGroups();
    });
  }
  decimalnumberWithCommas(x: any) {
    return this.reportService.decimalnumberWithCommas(x);
  }
}
