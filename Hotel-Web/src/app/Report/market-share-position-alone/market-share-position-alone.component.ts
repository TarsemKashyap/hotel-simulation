import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { MarketSharePositionAloneReportResponse } from '../model/MarketSharePositionAloneResponse.model';
import Chart from 'chart.js/auto';
import { possitionAloneReportAttribute } from '../model/ReportCommon.model';
import { Utility } from 'src/app/shared/utility';
import ChartDataLabels from 'chartjs-plugin-datalabels';
@Component({
  selector: 'app-market-share-position-alone',
  templateUrl: './market-share-position-alone.component.html',
  styleUrls: ['./market-share-position-alone.component.css'],
})
export class MarketSharePositionAloneComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  marketSharePositionAloneReportResponse: MarketSharePositionAloneReportResponse =
    {} as MarketSharePositionAloneReportResponse;
  public chart: Chart;
  ChartData: possitionAloneReportAttribute[] = [];

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
      .marketSharePositionAloneReportDetails(this.reportParam)
      .subscribe((reportData) => {
        let reportData2 = reportData.data.map((p) => {
          return {
            label: p.label,
            actualMarketShare: p.actualMarketShare * 100,
            marketSharePosition: p.marketSharePosition * 100,
          } as possitionAloneReportAttribute;
        });
        this.ChartData = [];
        this.marketSharePositionAloneReportResponse = reportData;
        this.ChartData = reportData2; //.marketSharePositionAloneReportResponse.data;
        this.YaxisMktShrPos = this.ChartData.map(
          (item) => item.marketSharePosition
        );
        this.YaxisActualMktShr = this.ChartData.map(
          (item) => item.actualMarketShare
        );
        this.Xaxis = this.ChartData.map((item) => item.label);

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
            label: 'Market share by position',
            data: this.YaxisMktShrPos,
            backgroundColor: 'skyblue',
          },
          {
            label: 'Actual Maket Share',
            data: this.YaxisActualMktShr,
            backgroundColor: 'orange',
          },
        ],
      },
      plugins: [ChartDataLabels],
      options: {
        scales: {
          y: {
            ticks: {
              callback: function (tickValue, index, ticks) {
                return `${tickValue}%`;
              },
            },
          },
        },
        aspectRatio: 4,
        responsive: true,
        plugins: {
          datalabels: {
            formatter: (value, context) => {
              return Utility.ToPercent(value/100);
            },
          },
        },
      },
    });
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
}
