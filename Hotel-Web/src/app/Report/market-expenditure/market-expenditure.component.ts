import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import {MarketExpenditureReportResponse} from '../model/MarketExpenditureResponse.model';
import Chart from 'chart.js/auto';
import {MarketExpenditureReportAttribute } from "../model/ReportCommon.moel";

@Component({
  selector: 'app-market-expenditure',
  templateUrl: './market-expenditure.component.html',
  styleUrls: ['./market-expenditure.component.css']
})

export class MarketExpenditureComponent {
  MonthList : MonthDto[] = [];
  selectedMonth : MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;

  reportParam:ReportParams = {} as ReportParams;
  marketExpenditureReportResponse : MarketExpenditureReportResponse = {} as MarketExpenditureReportResponse;
  public chart: any;
  ChartData:MarketExpenditureReportAttribute[]=[];
 
  YaxisData:any[]=[];
  Xaxis:any[]=[];
  YaxisSaleForce:any[]=[];
  YaxisAdv:any[]=[];
 
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
    this.reportService.marketExpenditureReportDetails(this.reportParam).subscribe((reportData) => {
      // console.log('DATA...........');
      
      // console.log(reportData);
        this.marketExpenditureReportResponse = reportData;  
        
       this.ChartData=this.marketExpenditureReportResponse.segments;
       
      this.YaxisData.push.apply(this.YaxisData,this.ChartData);
          this.YaxisSaleForce.push.apply(this.YaxisSaleForce,this.ChartData.map(i=>i.labor.salesForce));
     this.YaxisAdv.push.apply(this.YaxisAdv,this.ChartData.map(i=>i.labor.advertising));
          this.Xaxis=this.ChartData.map(item=>item.label);
        
          console.log('this.YaxisAdv',this.YaxisAdv)
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
            label: "SaleForce",
            data: this.YaxisSaleForce,
            backgroundColor: 'blue'
          },
          {
            label: "Advertisement",
            data: this.YaxisAdv,
            backgroundColor: 'limegreen'
          }  
        ]
      },
      options: {
        aspectRatio:2.5
      }
      
    });
  }
  numberToDecimal(x:any)
  {
    return this.reportService.numberToDecimal(x);
  }
  decimalnumberWithCommas(x:any) {
    return this.reportService.decimalnumberWithCommas(x);
   
  }
}