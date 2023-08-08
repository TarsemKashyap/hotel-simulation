import { Component } from '@angular/core';
import { MonthDto } from 'src/app/shared/class/create-month/month.model';
import { ClassGroup } from 'src/app/shared/class/model/classSession.model';
import { ReportService } from '../report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportParams } from '../model/ReportParams.model';
import { PerformanceResponse } from '../model/PerformanceResponse.model';

@Component({
  selector: 'app-performance',
  templateUrl: './performance.component.html',
  styleUrls: ['./performance.component.css']
})
export class PerformanceComponent {
  selectedMonth : MonthDto = {} as MonthDto;
  MonthList : MonthDto[] = [];
  groups: ClassGroup[] = [];
  selectedHotel : ClassGroup | undefined;
  classId: number | undefined;
  reportParam : ReportParams = {} as ReportParams;
  performancereportResponse : PerformanceResponse = {} as PerformanceResponse;

  constructor(
    private reportService: ReportService,
    private router: Router,   
    public activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.classId = this.activeRoute.snapshot.params['id'];
    this.loadMonths();
  }

  onOptionChange() {
    
  }

  loadPerformanceDetails() {    
    this.reportParam.ClassId =  this.classId!;
    this.reportParam.GroupId =this.selectedHotel?.serial!;
    this.reportParam.MonthId = parseInt(this.selectedMonth.monthId!);
    this.reportParam.CurrentQuarter = 0;
    this.reportService.performanceReportDetails(this.reportParam).subscribe((reportData) => {
        this.performancereportResponse = reportData;  
        console.log( this.performancereportResponse," this.performancereportResponse")      
    });
  }

  private loadMonths() {
    this.reportService.monthFilterList(this.classId!).subscribe((months) => {
      this.MonthList = months;  
      this.selectedMonth = this.MonthList.at(this.MonthList.length -1)!;
      this.loadGroups();
    });
  }

  private loadGroups() {
    this.reportService.groupFilterList(this.classId!).subscribe((groups) => {
      this.groups = groups;
      this.selectedHotel =  this.groups.at(0);
      this.loadPerformanceDetails();
    });
  }
}
