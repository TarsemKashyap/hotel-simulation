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
} from '../model/ReportCommon.moel';

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
  YaxisAdv: any[] = [];

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
      { name: 'Advertising', value: 'Advertising' },
      { name: 'Sales Force', value: 'Sales Force' },
      { name: 'Promotions', value: 'Promotions' },
      { name: 'Public Relations', value: 'Public Relations' }
    );
    this.selectedSectorlist.push(
      { name: 'Labor', value: 'Labor' },
      { name: 'Other', value: 'Other' }
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

    this.reportService
      .marketExpenditureReportDetails(this.reportParam)
      .subscribe((reportData) => {
        // console.log('DATA...........');

        // console.log(reportData);
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
          } else if (
            this.selectedMarketingStrategy?.value == 'Public Relations'
          ) {
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
        //this.YaxisData.push.apply(this.YaxisData,this.ChartData);

        //  this.YaxisAdv.push.apply(this.YaxisAdv,this.ChartData.map(i=>i.labor.advertising));

        console.log('this.YaxisAdv', this.YaxisAdv);
        console.log('this.YaxisSaleforce', this.YaxisSaleForce);
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
            label: this.chartLabel,
            data: this.YaxisSaleForce,
            backgroundColor: 'skyblue',
            type: 'bar',
          },
          {
            label: this.chartLabel,
            data: this.YaxisSaleForce,
            backgroundColor: 'red',
            borderColor: 'red',
            type: 'line',
            order:1
          },
        ],
      },
      options: {
        aspectRatio: 2.5,
        plugins: {
          legend: {
            position: 'top',
          }
          
        }
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
