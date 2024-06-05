import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { RevParGopalReportResponse } from '../model/RevParGoparResponse.model';
import Chart from 'chart.js/auto';
import { avgdailyrateReportAttribute } from '../model/ReportCommon.model';
import { isNgTemplate } from '@angular/compiler';
@Component({
  selector: 'app-rev-par-gopar',
  templateUrl: './rev-par-gopar.component.html',
  styleUrls: ['./rev-par-gopar.component.css'],
})
export class RevParGoparComponent {
  MonthList: MonthDto[] = [];
  selectedMonth: MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel: ClassGroup | undefined;

  reportParam: ReportParams = {} as ReportParams;
  revParGopalReportResponse: RevParGopalReportResponse =
    {} as RevParGopalReportResponse;
  public chart: Chart;
  ChartData: avgdailyrateReportAttribute[] = [];
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
      .revParGopalReportDetails(this.reportParam)
      .subscribe((reportData) => {
        this.revParGopalReportResponse = reportData;
        this.ChartData.push(this.revParGopalReportResponse.overAll);
        this.ChartData.push.apply(
          this.ChartData,
          this.revParGopalReportResponse.overAllChild
        );
        this.ChartData.push(this.revParGopalReportResponse.totalRevpar);
        this.ChartData.push(this.revParGopalReportResponse.goPar);

        this.YaxisMarketAvg = this.ChartData.map((item) => item.marketAvg);
        this.YaxisHotel = this.ChartData.map((item) => item.hotel);
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
            label: 'Market Average',
            data: this.YaxisMarketAvg,
            backgroundColor: 'Red',
            borderColor: 'Red',
            type: 'line',
          },
          {
            label: 'hotel',
            data: this.YaxisHotel,
            backgroundColor: 'skyblue',
          },
        ],
      },
      options: {
        aspectRatio: 2.5,
        indexAxis: 'x',
        scales: {
          y: {
            ticks: { crossAlign: 'far' },
          },
         
        },
      },
    });
  }
  numberWithCommas(x: any) {
    return this.reportService.numberWithCommas;
  }
  numberToDecimal(x: any) {
    return this.reportService.numberToDecimal(x);
  }
}
