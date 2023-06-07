import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
//import { MonthService } from './month.service';
//import { MonthDto } from './month.model';
import { MonthService } from './month.service';

import { MonthDto } from './month.model';
import { QuarterlyMarketDto } from './quarterly-market.model';
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
  quarterlyMarketList:QuarterlyMarketDto[]=[];
  dataSource = new MatTableDataSource<MonthDto>();
  dataSourceQuarterlyMarket = new MatTableDataSource<QuarterlyMarketDto>();
  configId='2853c04b-3f2d-4e4c-b930-a7fc924871df'
  currentQuarter=1;
  MarketTextBox:string='';
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
      this.quarterlyMarketList = data;
      this.currentQuarter=data.length;
      console.log(this.quarterlyMarketList[0]);
      this.dataSourceQuarterlyMarket.data = data;
    });
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  };
  
  CreateNewQuarter(){
    console.log('it does nothing',this.MarketTextBox);
  }
}
