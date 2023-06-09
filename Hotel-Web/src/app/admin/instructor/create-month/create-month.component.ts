import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
//import { MonthService } from './month.service';
//import { MonthDto } from './month.model';
import { MonthService } from './month.service';

import { MonthDto } from './month.model';
import {ClassDto} from './month.model';
//import { QuarterlyMarketDto } from './quarterly-market.model';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-month',
  
  templateUrl: './create-month.component.html',
  styleUrls: ['./create-month.component.css']
})
export class CreateMonthComponent {
  monthList: MonthDto[] = [];
  classInfo:ClassDto={
    classId: '',
    title: '',
    memo: '',
    startDate: '',
    endDate: '',
    hotelsCount: '',
    roomInEachHotel: '',
    currentQuater: '',
    createdOn: '',
    code: '',
    createdBy: '',
    status: ''
  };
  monthInfo:MonthDto[]=[];
  classId=1;
  monthId:any=0;
  
  dataSource = new MatTableDataSource<MonthDto>();
  dataSourceMonth = new MatTableDataSource<MonthDto>();
  configId='2853c04b-3f2d-4e4c-b930-a7fc924871df'
  currentQuarter:string='1';
  MarketTextBox:string='';
  apiBody={};
  displayedColumns: string[] = [
    'MonthId',
    'ClassId',
    'Sequence',
    'TotalMarket',
    'IsComplete',
    'ConfigId'
  ];
  constructor(
    private monthService: MonthService,
    private router: Router,
    public dialog: MatDialog,
    public snackBar: MatSnackBar
  ) {}
  @ViewChild(MatSort)
  sort!: MatSort;
  ngOnInit(): void {

    this.monthService.quarterlyMarketList().subscribe((data) => {
      this.monthList = data;
     // this.currentQuarter=data.length;
      console.log(this.monthList[0]);
      this.dataSourceMonth.data = data;
      });

      this.monthService.classInfo(this.classId).subscribe((data)=>{
        this.classInfo=data;
        debugger;
        this.currentQuarter=this.classInfo.currentQuater;
        console.log(this.classInfo);
        this.monthService.monthInfo(this.classId,this.currentQuarter).subscribe((data)=>{
          this.monthInfo=data;
          console.log(this.monthInfo);
        });
      });
      

  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  };
  
  CreateNewMonth(){
    console.log('it does nothing',this.MarketTextBox);
    this.apiBody={"ClassId":1,"TotalMarket":this.MarketTextBox};
    this.monthService.createNewMonth(this.apiBody).subscribe((data) => {
     
      console.log("MonthID:="+data.data.monthId);
      console.log(data.message);
      //console.log(data.Data.monthID);
    });
  }
}
