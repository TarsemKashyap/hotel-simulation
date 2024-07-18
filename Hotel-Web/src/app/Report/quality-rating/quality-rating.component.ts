import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { QualityRatingReportResponse } from '../model/QualityRatingResponse.model';
import Chart from 'chart.js/auto';
import { QualityRatingAttribute } from '../model/ReportCommon.model';
import { SeedData, Segment } from '../model/Segment';
@Component({
  selector: 'app-quality-rating',
  templateUrl: './quality-rating.component.html',
  styleUrls: ['./quality-rating.component.css'],
})
export class QualityRatingComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;
  segments: Segment[] = SeedData.SegmentList();
  selectedSegment: Segment | undefined;
  reportParam: ReportParams = {} as ReportParams;
  qualityRatingReportResponse: QualityRatingReportResponse =
    {} as QualityRatingReportResponse;
  public chart: Chart;
  ChartData: QualityRatingAttribute[] = [];

  YaxisData: any[] = [];
  Xaxis: any[] = [];
  YaxisHotel: any[] = [];
  YaxisMarketAvg: any[] = [];

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
    this.reportParam.Segment = this.selectedSegment?.value!;
    this.reportService
      .qualityRatingReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.qualityRatingReportResponse = reportData;
        this.ChartData = this.qualityRatingReportResponse.segments;
        this.YaxisData=[];
        this.YaxisHotel=[];
        this.YaxisMarketAvg=[];
        this.YaxisData.push(reportData.overAll, ...reportData.segments);
        this.YaxisHotel = this.YaxisData.map((i) => i.hotel);
        this.YaxisMarketAvg = this.YaxisData.map((i) => i.marketAverage);
        this.Xaxis = this.YaxisData.map((item) => item.label);

        this.createChart();
      });
  }

  private loadGroups() {
    this.selectedSegment = this.segments.at(0);
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
  createChart() {
    if (this.chart) {
      this.chart.destroy();
    }
    this.chart = new Chart('MyChart', {
      type: 'bar', //this denotes tha type of chart

      data: {
        // values on X-Axis
        labels: this.Xaxis,
        datasets: [
          {
            label: 'Hotel',
            data: this.YaxisHotel,
            backgroundColor: 'skyblue',
          },
          {
            label: 'Market Average',
            data: this.YaxisMarketAvg,
            backgroundColor: 'orange',
          },
        ],
      },

      options: {
        aspectRatio: 4,
      },
    });
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
  decimalnumberWithCommas(x: any) {
    return this.reportService.decimalnumberWithCommas(x);
  }
}
