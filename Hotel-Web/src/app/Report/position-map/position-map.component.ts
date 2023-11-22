import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { PositionMapReportResponse } from '../model/PositionMapResponse.model';
import Chart from 'chart.js/auto';
import { PositionMapAttribute } from '../model/ReportCommon.model';
import { SeedData, Segment } from '../model/Segment';
import ChartDataLabels from 'chartjs-plugin-datalabels';
@Component({
  selector: 'app-position-map',
  templateUrl: './position-map.component.html',
  styleUrls: ['./position-map.component.css'],
})
export class PositionMapComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  segments: Segment[] = SeedData.SegmentWithOverAll();
  selectedSegment: Segment | undefined;

  reportParam: ReportParams = {} as ReportParams;
  positionMapReportResponse: PositionMapReportResponse =
    {} as PositionMapReportResponse;
  public chart: Chart | any;
  ChartData: PositionMapAttribute[] = [];

  YaxisData: any[] = [];
  Xaxis: any[] = [];
  YaxisQualityRating: any[] = [];
  YaxisRoomRating: any[] = [];

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
    this.reportParam.Segment = this.selectedSegment?.value!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = parseInt(this.selectedMonth.sequence!);
    this.reportService
      .positionMapReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.positionMapReportResponse = reportData;
        this.ChartData = this.positionMapReportResponse.groupRating;
        // this.YaxisData.push.apply(this.YaxisData, this.ChartData);
        // this.YaxisQualityRating.push.apply(
        //   this.YaxisQualityRating,
        //   this.ChartData.map((i) => i.qualityRating)
        // );
        // this.YaxisRoomRating.push.apply(
        //   this.YaxisRoomRating,
        //   this.ChartData.map((i) => i.roomRate)
        // );
        // this.Xaxis = this.ChartData.map((item) => item.roomRate);

        this.createChart(reportData.groupRating);
      });
  }

  private loadGroups() {
    this.selectedSegment = this.segments.at(0);
    this.loadReportDetails();
    // this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
    //   this.segments = groups;
    //   this.selectedSegment = this.segments.at(0);
    // });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;
      this.selectedMonth = this.MonthList.at(this.MonthList.length - 1)!;
      this.loadGroups();
    });
  }
  createChart(data: PositionMapAttribute[]) {
    var datasets = data.map((x, index) => {
      return {
        label: x.classGroup,
        pointRadius: 10,
        data: [
          {
            y: <any>data[index].qualityRating,
            x: <any>data[index].roomRate,
          },
        ],
      };
    });
    console.log(datasets);
    this.chart = new Chart('MyChart', {
      type: 'scatter', //this denotes tha type of chart

      data: {
        datasets: datasets,
      },
      plugins: [ChartDataLabels],
      options: {
        aspectRatio: 2.5,
        plugins: {
          datalabels: {
            formatter: (value, context) => {
              let data = <any>context.dataset.data[0];
              return `${context.dataset.label} ($${this.numberToDecimal(
                data!.x
              )},${this.numberToDecimal(data!.y)})`;
            },
            align: 'top',
            offset: 10,
            padding: 5,
            display: 'auto',
          },
        },
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
