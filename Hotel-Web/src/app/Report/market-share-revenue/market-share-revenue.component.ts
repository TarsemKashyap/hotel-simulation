import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { MarketShareRevenueReportResponse } from '../model/MarketShareRevenueResponse.model';
import Chart from 'chart.js/auto';
import {
  occupancyReportAttribute,
  IoccupancyBySegment,
} from '../model/ReportCommon.model';
import { ChartConfig } from 'src/app/shared/utility';
@Component({
  selector: 'app-market-share-revenue',
  templateUrl: './market-share-revenue.component.html',
  styleUrls: ['./market-share-revenue.component.css'],
})
export class MarketShareRevenueComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  marketShareRevenueReportResponse: MarketShareRevenueReportResponse =
    {} as MarketShareRevenueReportResponse;
  public chart: Chart;
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
      .marketShareRevenueReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.marketShareRevenueReportResponse = reportData;
        this.occupancyBySegment =
          this.marketShareRevenueReportResponse.occupancyBySegment;
        this.overAllPercentages =
          this.marketShareRevenueReportResponse.overAllPercentages;
        this.occupancyBySegmentSeg = this.occupancyBySegment.map(
          (i) => i.segments
        );
        this.ChartData =
          this.marketShareRevenueReportResponse.occupancyBySegment;
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
            label: 'Market Average',
            data: this.YaxisMarketAvg,
            backgroundColor: ChartConfig.LineBgColor,
            type: 'line',
            borderColor: 'red',
          },
          {
            label: 'Hotel',
            data: this.YaxisHotel,
            backgroundColor: ChartConfig.BarBgColor,
            type: 'bar',
            barPercentage: ChartConfig.BarThickness,
            order:1
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
}
