import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import {MarketShareRoomSoldReportResponse} from '../model/MarketShareRoomSoldResponse.model';
@Component({
  selector: 'app-market-share-roomsold',
  templateUrl: './market-share-roomsold.component.html',
  styleUrls: ['./market-share-roomsold.component.css']
})
export class MarketShareRoomSoldComponent {
  MonthList : MonthDto[] = [];
  selectedMonth : MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;

  reportParam:ReportParams = {} as ReportParams;
  marketShareRoomSoldReportResponse : MarketShareRoomSoldReportResponse = {} as MarketShareRoomSoldReportResponse;

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
    this.reportService.marketShareRoomSoldReportDetails(this.reportParam).subscribe((reportData) => {
      // console.log('DATA...........');
      
      // console.log(reportData);
        this.marketShareRoomSoldReportResponse = reportData;  
        console.log('DataLenght',this.marketShareRoomSoldReportResponse);      
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
}
