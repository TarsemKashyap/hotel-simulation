import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import {PositionMapReportResponse} from '../model/PositionMapResponse.model';
import Chart from 'chart.js/auto';
import {PositionMapAttribute } from "../model/ReportCommon.moel";

@Component({
  selector: 'app-position-map',
  templateUrl: './position-map.component.html',
  styleUrls: ['./position-map.component.css']
})
export class PositionMapComponent {
  MonthList : MonthDto[] = [];
  selectedMonth : MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;

  reportParam:ReportParams = {} as ReportParams;
  positionMapReportResponse : PositionMapReportResponse = {} as PositionMapReportResponse;
  public chart: any;
  ChartData:PositionMapAttribute[]=[];
 
  YaxisData:any[]=[];
  Xaxis:any[]=[];
  YaxisQualityRating:any[]=[];
  YaxisRoomRating:any[]=[];
 
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
    this.reportService.positionMapReportDetails(this.reportParam).subscribe((reportData) => {
      // console.log('DATA...........');
      
      // console.log(reportData);
        this.positionMapReportResponse = reportData;  
        
       this.ChartData=this.positionMapReportResponse.groupRating;
       
      this.YaxisData.push.apply(this.YaxisData,this.ChartData);
          this.YaxisQualityRating.push.apply(this.YaxisQualityRating,this.ChartData.map(i=>i.qualityRating));
     this.YaxisRoomRating.push.apply(this.YaxisRoomRating,this.ChartData.map(i=>i.roomRate));
          this.Xaxis=this.ChartData.map(item=>item.roomRate);
        
          console.log('this.YaxisAdv',this.YaxisRoomRating)
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
            label: "QualityRating",
            data: this.YaxisQualityRating,
            backgroundColor: 'blue'
          },
          // {
          //   label: "Advertisement",
          //   data: this.YaxisRoomRating,
          //   backgroundColor: 'limegreen'
          // }  
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
