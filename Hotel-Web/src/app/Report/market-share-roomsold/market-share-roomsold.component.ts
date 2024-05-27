import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { MarketShareRoomSoldReportResponse } from '../model/MarketShareRoomSoldResponse.model';
import Chart from 'chart.js/auto';
import {
  occupancyReportAttribute,
  IoccupancyBySegment,
} from '../model/ReportCommon.model';
import { ChartConfig } from 'src/app/shared/utility';
@Component({
  selector: 'app-market-share-roomsold',
  templateUrl: './market-share-roomsold.component.html',
  styleUrls: ['./market-share-roomsold.component.css'],
})
export class MarketShareRoomSoldComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  marketShareRoomSoldReportResponse: MarketShareRoomSoldReportResponse =
    {} as MarketShareRoomSoldReportResponse;
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
      .marketShareRoomSoldReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.marketShareRoomSoldReportResponse = reportData;
        this.occupancyBySegment =
          this.marketShareRoomSoldReportResponse.occupancyBySegment;
        this.overAllPercentages =
          this.marketShareRoomSoldReportResponse.overAllPercentages;
        this.occupancyBySegmentSeg = this.occupancyBySegment.map(
          (i) => i.segments
        );
        this.ChartData =
          this.marketShareRoomSoldReportResponse.occupancyBySegment;
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
            label: 'Market Average',
            data: this.YaxisMarketAvg,
            backgroundColor: 'red',
            borderColor: 'red',
            type: 'line',
          },
          {
            order: 1,
            label: 'Hotel',
            data: this.YaxisHotel,
            backgroundColor: 'skyblue',
            barPercentage: ChartConfig.BarThickness,
          },
        ],
      },
      options: {
        //responsive:true,
        aspectRatio: 3,
      },
    });
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
}
