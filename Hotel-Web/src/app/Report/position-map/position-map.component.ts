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
import { JsonPipe } from '@angular/common';
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
    const colors = ['red', 'green', 'blue', 'brown', 'orange'];

    var datasets = data.map((x, index) => {
      return {
        label: x.classGroup,
        backgroundColor: () => {
          return colors[index];
        },
        pointRadius: 5 + index * 5,
        offset: (index + 5) * 25,
        borderRadius: 23,
        borderWidth: 10,
        data: [
          {
            y: <any>data[index].qualityRating,
            x: <any>data[index].roomRate,
          },
        ],
      };
    });
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
            align: (context) => {
              return (context.datasetIndex + 1) * 70;
            },
            offset: 25,
            color: (context) => {
              return colors[context.datasetIndex];
            },
            clamp: true,
            //padding: 5,
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
