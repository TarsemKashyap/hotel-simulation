import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { MarketExpenditureReportResponse } from '../model/MarketExpenditureResponse.model';
import Chart from 'chart.js/auto';
import {
  MarketExpenditureReportAttribute,
  Sector,
  MarketingStrategy,
} from '../model/ReportCommon.model';
import { tick } from '@angular/core/testing';
import { ChartConfig, Utility } from 'src/app/shared/utility';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { CurrencyPipe, formatCurrency } from '@angular/common';

@Component({
  selector: 'app-market-expenditure',
  templateUrl: './market-expenditure.component.html',
  styleUrls: ['./market-expenditure.component.css'],
})
export class MarketExpenditureComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;
  selectedMarketingStrategyList: MarketingStrategy[] = [];
  selectedSectorlist: Sector[] = [];
  selectedSector: Sector | undefined;
  chartLabel: string = '';

  selectedMarketingStrategy: MarketingStrategy | undefined;
  reportParam: ReportParams = {} as ReportParams;
  marketExpenditureReportResponse: MarketExpenditureReportResponse =
    {} as MarketExpenditureReportResponse;
  public chart: any;
  ChartData: MarketExpenditureReportAttribute[] = [];

  YaxisData: any[] = [];
  Xaxis: any[] = [];
  YaxisSaleForce: any[] = [];
  YMketAvg: any[] = [];
  YaxisAdv: any[] = [];
  private fieldMap = { '': '' };

  constructor(
    private reportService: ReportService,
    private router: Router,
    public snackBar: MatSnackBar,
    public activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.classId = this.activeRoute.snapshot.params['id'];
    this.loadMonths();
    this.selectedMarketingStrategyList.push(
      { name: 'Advertising', value: 'Advertising', field: 'advertising' },
      { name: 'Sales Force', value: 'Sales Force', field: 'salesForce' },
      { name: 'Promotions', value: 'Promotions', field: 'promotions' },
      {
        name: 'Public Relations',
        value: 'Public Relations',
        field: 'publicRelations',
      }
    );
    this.selectedSectorlist.push(
      { name: 'Labor', value: 'Labor', field: 'labor' },
      { name: 'Other', value: 'Other', field: 'other' }
    );

    this.selectedMarketingStrategy = this.selectedMarketingStrategyList.at(0);
    this.selectedSector = this.selectedSectorlist.at(0);
    //  this.selectedMarketingStrategy=""
  }

  onOptionChange() {
    let chartStatus = Chart.getChart('MyChart'); // <canvas> id
    if (chartStatus != undefined) {
      chartStatus.destroy();
    }
    this.loadReportDetails();
  }

  loadReportDetails() {
    this.reportParam.ClassId = this.classId!;
    this.reportParam.GroupId = this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = parseInt(this.selectedMonth.sequence!);
    this.reportParam.Segment = this.selectedMarketingStrategy?.value!;
    this.reportParam.Sector = this.selectedSector?.value!;

    this.reportService
      .marketExpenditureReportDetails(this.reportParam)
      .subscribe((reportData) => {
        //this.oldMethod(reportData);
        this.newChartWay(reportData);
      });
  }

  private newChartWay(reportData: MarketExpenditureReportResponse) {
    this.marketExpenditureReportResponse = reportData;
    this.ChartData = this.marketExpenditureReportResponse.segments.filter(
      (x) => x.label != null
    );

    this.Xaxis = this.ChartData.map((item) => item.label);
    this.chartLabel = this.selectedMarketingStrategy?.name!;
    this.YaxisSaleForce = this.ChartData.map((i: any) => {
      let sector = i[this.selectedSector!.field];
      let stragy = sector[this.selectedMarketingStrategy!.field];
      return stragy;
    });
    this.YMketAvg = this.ChartData.map((i: any) => {
      let sector = i[this.selectedSector!.field];
      let stragy = sector[this.selectedMarketingStrategy!.field];
      return sector.marketAvg;
    });
    this.createChart();
   
  }

  private oldMethod(reportData: MarketExpenditureReportResponse) {
    this.marketExpenditureReportResponse = reportData;
    this.ChartData = this.marketExpenditureReportResponse.segments.filter(
      (x) => x.label != null
    );

    this.Xaxis = this.ChartData.map((item) => item.label);

    if (this.selectedSector?.value == 'Labor') {
      this.YaxisSaleForce = [];

      if (this.selectedMarketingStrategy?.value == 'Advertising') {
        this.chartLabel = 'Advertising';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.labor.advertising)
        );
      } else if (this.selectedMarketingStrategy?.value === 'Sales Force') {
        this.chartLabel = 'Sales Force';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.labor.salesForce)
        );
      } else if (this.selectedMarketingStrategy?.value == 'Promotions') {
        this.chartLabel = 'Promotions';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.labor.promotions)
        );
      } else if (this.selectedMarketingStrategy?.value == 'Public Relations') {
        this.chartLabel = 'Public Relations';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.labor.publicRelations)
        );
      }

      // }
    } else {
      this.YaxisSaleForce = [];
      // switch(this.selectedMarketingStrategy?.name)
      // {
      if (
        this.selectedMarketingStrategy?.name ==
        this.selectedMarketingStrategyList.at(0)?.name
      ) {
        this.chartLabel = 'Advertising';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.other.advertising)
        );
      }
      if (
        this.selectedMarketingStrategy?.name ==
        this.selectedMarketingStrategyList.at(1)?.name
      ) {
        this.chartLabel = 'Sales Force';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.other.salesForce)
        );
      }
      if (this.selectedMarketingStrategy?.name == 'Promotions') {
        this.chartLabel = 'Promotions';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.other.promotions)
        );
      }
      if (this.selectedMarketingStrategy?.name == 'Public Relations') {
        this.chartLabel = 'Public Relations';
        this.YaxisSaleForce.push.apply(
          this.YaxisSaleForce,
          this.ChartData.map((i) => i.other.publicRelations)
        );
      }

      //  }
    }
    this.createChart();
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
            label: this.chartLabel,
            data: this.YaxisSaleForce,
            backgroundColor: 'skyblue',
            type: 'bar',
            order: 1,
            barPercentage: ChartConfig.BarThickness,
          },
          {
            label: 'Market Average',
            data: this.YMketAvg,
            backgroundColor: 'red',
            borderColor: 'red',
            type: 'line',
          },
        ],
      },
      plugins: [ChartDataLabels],
      options: {
        aspectRatio: 4,
        plugins: {
          datalabels: {
            formatter: (value, context) => {
              return Utility.ToCurrency(value);
            },
            color: 'black',
            align: 'top',
            anchor: 'center',
            display: 'auto',
            offset: 5,
            clamp: true,
            padding: 5,
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
