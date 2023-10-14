import { Component } from '@angular/core';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportParams } from '../model/ReportParams.model';
import { GoalReportResponse } from '../model/GoalReportResponse.model';
import { find } from 'rxjs';


@Component({
  selector: 'app-objective-report',
  templateUrl: './objective-report.component.html',
  styleUrls: ['./objective-report.component.css']
})
export class ObjectiveReportComponent {
  MonthList : MonthDto[] = [];
  selectedMonth : MonthDto = {} as MonthDto;
  classId: number | undefined;
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;

  reportParam:ReportParams = {} as ReportParams;
  goalReportResponse : GoalReportResponse[] = [];

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
     this.loadObjectiveDetails();
   }

  loadObjectiveDetails() {    
    this.reportParam.ClassId =  this.classId!;
    this.reportParam.GroupId =this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = this.selectedHotel?.serial!;
    this.reportService.objectiveReportDetails(this.reportParam).subscribe((reportData) => {
        this.goalReportResponse = reportData;        
    });
  }

  private loadGroups() {
    this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
      this.groups = groups;
      this.selectedHotel =  this.groups.at(0);
      this.loadObjectiveDetails();
    });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;  
      this.selectedMonth = this.MonthList.at(this.MonthList.length -1)!;
      this.loadGroups();
    });
  }
  numberToDecimal(x:any)
  {
    return this.reportService.numberToDecimal(x);
  }
  numberWithCommas(x:any) {
    return this.reportService.numberWithCommas(x);
}
}
