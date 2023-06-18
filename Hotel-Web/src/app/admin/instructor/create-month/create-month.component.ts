import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
//import { MonthService } from './month.service';
//import { MonthDto } from './month.model';
import { MonthService } from './month.service';

import { MonthDto } from './month.model';
import { ClassDto } from './month.model';
//import { QuarterlyMarketDto } from './quarterly-market.model';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-month',

  templateUrl: './create-month.component.html',
  styleUrls: ['./create-month.component.css'],
})
export class CreateMonthComponent {
  monthList: MonthDto[] = [];
  classInfo: ClassDto = {
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
    status: '',
  };
  monthInfo: MonthDto = {
    monthId: '',
    classId: '',
    sequence: '',
    totalMarket: '',
    isComplete: false,
    configId: '',
  };
  classId = 0;
  monthId: any = 0;
  QuarterNoLabel: string = '';
  isMonthCompleted: boolean = false;
  isNewQuarterButtonDisable = true;
  isFinalizeButtonDisable = true;
  MessageLabel: string = '';
  isError: boolean = false;
  errorMsg: string = 'Required Field';
  dataSource = new MatTableDataSource<MonthDto>();
  dataSourceMonth = new MatTableDataSource<MonthDto>();
  configId = '2853c04b-3f2d-4e4c-b930-a7fc924871df';
  currentQuarter: Number = 0;
  MarketTextBox: string = '';
  apiBody = {};
  displayedColumns: string[] = [
    'MonthId',
    'ClassId',
    'Sequence',
    'TotalMarket',
    'IsComplete',
    'ConfigId',
  ];
  constructor(
    private monthService: MonthService,
    private router: ActivatedRoute,
    public dialog: MatDialog,
    public snackBar: MatSnackBar
  ) {
    
  }
  @ViewChild(MatSort)
  sort!: MatSort;
  ngOnInit(): void {
    this.router.params.subscribe(x=>{
      this.classId=x['id'];
    })
    this.pageload();
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  pageload() {
    this.monthService.quarterlyMarketList().subscribe((data) => {
      this.monthList = data;

      console.log(this.monthList[0]);
      this.dataSourceMonth.data = data;
    });

    this.monthService.classInfo(this.classId).subscribe((data) => {
      this.classInfo = data;
      this.currentQuarter = Number(this.classInfo.currentQuater);
      console.log(this.classInfo);
      this.monthService
        .monthInfo(this.classId, this.currentQuarter)
        .subscribe((data) => {
          this.monthInfo = data;
          this.isMonthCompleted = this.monthInfo.isComplete;
          console.log(this.monthInfo);

          if (this.currentQuarter != 0) {
            this.QuarterNoLabel = String(Number(this.currentQuarter) + 1);
            // ifComplete = Convert.ToBoolean(quarterAdapter.ScalarQueryIfCompleted((Guid)Session["session"], currentQuarter));
            if (this.isMonthCompleted == false) {
              this.isNewQuarterButtonDisable = true;
              this.MessageLabel =
                'Month ' +
                this.currentQuarter +
                " hasn't finished. You can't create new month at this moment.";
            }
          } else if (this.currentQuarter == 0) {
            this.isNewQuarterButtonDisable = false;
            this.MessageLabel = 'No month has been created.';
          }
          if (this.classInfo.status != 'T') {
            this.isFinalizeButtonDisable = true;
          }
          ////"C" means that calucation is not finished yet.
          if (this.classInfo.status == 'C') {
            this.isFinalizeButtonDisable = true;
            this.isNewQuarterButtonDisable = true;
          }
        });
    });
  }
  CreateNewMonth() {
    console.log('it does nothing', this.MarketTextBox);
    if (this.MarketTextBox.length == 0) {
      this.isError = true;
      this.errorMsg = 'Required Field';
      return;
    }
    this.isNewQuarterButtonDisable = true;
    this.apiBody = { ClassId: this.classId, TotalMarket: this.MarketTextBox };
    this.monthService.createNewMonth(this.apiBody).subscribe((data) => {
      this.isError = false;
      this.errorMsg = '';
      this.pageload();
      console.log('MonthID:=' + data.data.monthId);
      console.log(data.message);
      //console.log(data.Data.monthID);
    });
  }
  FinalizeMonth() {
    this.apiBody = {
      ClassId: this.classId,
      Sequence: this.currentQuarter,
      IsComplete: true,
    };
    this.monthService
      .updateMonthCompletedStatus(this.apiBody)
      .subscribe((data) => {
        console.log('isCompletedDone:=' + data);
        //console.log(data.message);
        //console.log(data.Data.monthID);
        this.apiBody = { ClassId: this.classId, Status: 'A' };
        this.monthService.UpdateClassStatus(this.apiBody).subscribe((data) => {
          console.log('isClass CompletedDone:=' + data);
          //console.log(data.message);
          //console.log(data.Data.monthID);
        });
      });
  }
}
