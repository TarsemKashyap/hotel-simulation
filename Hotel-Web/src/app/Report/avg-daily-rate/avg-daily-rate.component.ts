import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import {AvgDailyRateReportResponse} from '../model/AvgDailyRateResponse.model';
import { find } from 'rxjs';
import Chart from 'chart.js/auto';
import { ReportCommon,ReportAttribute,avgdailyrateReportAttribute } from "../model/ReportCommon.moel";

@Component({
  selector: 'app-avg-daily-rate',
  templateUrl: './avg-daily-rate.component.html',
  styleUrls: ['./avg-daily-rate.component.css']
})
export class AvgDailyRateComponent {
  MonthList : MonthDto[] = [];
  selectedMonth : MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;

  reportParam:ReportParams = {} as ReportParams;
  avgDailyRateReportResponse : AvgDailyRateReportResponse = {} as AvgDailyRateReportResponse;
  public chart: any;
  ChartData:avgdailyrateReportAttribute[]=[];
  Xaxis:any[]=[];
  YaxisMarketAvg:any[]=[];
  YaxisHotel:any[]=[];
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
    this.reportParam.ClassId =  this.classId!;
    this.reportParam.GroupId =this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter =parseInt(this.selectedMonth.sequence!);
    this.reportService.avgDailyRateReportDetails(this.reportParam).subscribe((reportData) => {
      // console.log('DATA...........');
      
      // console.log(reportData);
        this.avgDailyRateReportResponse = reportData;
        
        this.ChartData=this.avgDailyRateReportResponse.data;
       this.YaxisMarketAvg=this.ChartData.map(item=>item.marketAvg);
       this.YaxisHotel=this.ChartData.map(item=>item.hotel);
       this.Xaxis=this.ChartData.map(item=>item.label);
        this.createChart();     
    });
  }

  private loadGroups() {
    this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
      this.groups = groups;
      this.selectedHotel =  this.groups.at(0);
      this.loadReportDetails();
    });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;  
      this.selectedMonth = this.MonthList.at(this.MonthList.length -1)!;
      this.loadGroups();
    });
  }
  createChart(){
  
    this.chart = new Chart("MyChart", {
      type: 'bar', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: this.Xaxis, 
	       datasets: [
          {
            label: "marketAvg",
            data: this.YaxisMarketAvg,
            backgroundColor: 'blue'
          },
          {
            label: "hotel",
            data: this.YaxisHotel,
            backgroundColor: 'limegreen'
          }  
        ]
      },
      options: {
        aspectRatio:2.5
      }
      
    });
  }
}
