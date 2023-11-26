import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { OccupancyReportResponse } from '../model/OccupancyResponse.model';
import { find } from 'rxjs';
import Chart from 'chart.js/auto';
import {
  occupancyReportAttribute,
  IoccupancyBySegment,
} from '../model/ReportCommon.model';

@Component({
  selector: 'app-occupancy',
  templateUrl: './occupancy.component.html',
  styleUrls: ['./occupancy.component.css'],
})
export class OccupancyComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  occupancyReportResponse: OccupancyReportResponse =
    {} as OccupancyReportResponse;
  public chart: any;
  ChartData: IoccupancyBySegment[] = [];
  occupancyBySegment: IoccupancyBySegment[] = [];
  occupancyBySegmentSeg: occupancyReportAttribute[][] = [];
  overAllPercentages: occupancyReportAttribute[] = [];
  YaxisData: any[] = [];
  Xaxis: any[] = [];
  YaxisMarketAvg: any[] = [];
  YaxisHotel: any[] = [];
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
      .occupancyReportDetails(this.reportParam)
      .subscribe((reportData) => {
        // console.log('DATA...........');

        // console.log(reportData);
        this.occupancyReportResponse = reportData;
        this.occupancyBySegment =
          this.occupancyReportResponse.occupancyBySegment;
        this.overAllPercentages =
          this.occupancyReportResponse.overAllPercentages;
        this.occupancyBySegmentSeg = this.occupancyBySegment.map(
          (i) => i.segments
        );
        this.ChartData = this.occupancyReportResponse.occupancyBySegment;
        for (let entry of this.occupancyBySegmentSeg) {
          this.YaxisData.push.apply(this.YaxisData, entry);
        }
        this.YaxisData.push.apply(this.YaxisData, this.overAllPercentages);
        this.YaxisMarketAvg = this.YaxisData.map((item) => item.marketAverage);
        this.YaxisHotel = this.YaxisData.map((item) => item.hotel);
        this.Xaxis = this.ChartData.map((item) => item.segmentTitle);

        this.createChart();
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
  createChart() {
    this.chart = new Chart('MyChart', {
      type: 'bar', //this denotes tha type of chart

      data: {
        // values on X-Axis
        labels: this.Xaxis,
        datasets: [
          {
            label: 'marketAvg',
            data: this.YaxisMarketAvg,
            backgroundColor: 'blue',
          },
          {
            label: 'hotel',
            data: this.YaxisHotel,
            backgroundColor: 'limegreen',
          },
        ],
      },
      options: {
        aspectRatio: 2.5,
      },
    });
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
  numberWithCommas(x: any) {
    return this.reportService.numberWithCommas(x);
  }
}
