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
import { GridActionComponent } from '../grid-action/grid-action.component';
import { RowAction, GridActionParmas } from '../grid-action/grid-action.model';
import { MonthCalculationService } from '../month-calculation/monthcalculation.service';

@Component({
  selector: 'app-create-month',

  templateUrl: './create-month.component.html',
  styleUrls: ['./create-month.component.css'],
})
export class CreateMonthComponent {
  $rows: MonthDto[] = [];
  // monthList: MonthDto[] = [];
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
    status: '',
    configId: '',
    isComplete: false,
  };
  classId: number | undefined;
  //classId: any;
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
  btnfinltext: string = 'Finalize Now';
  btnCreateNewMonth: string = 'Create a New Month';
  apiBody = {};

  columnDefs: ColDef[] = [
    {
      field: 'sequence',
      headerName: 'Sequence',
    },
    {
      field: 'totalMarket',
      headerName: 'Total Market',
    },
    {
      field: 'configId',
      headerName: 'Config Id',
    },
    { field: 'status', headerName: 'Status' },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    sortable: true,
  };

  constructor(
    private monthService: MonthService,
    private monthCalculationService: MonthCalculationService,
    private router: Router,
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    public route: ActivatedRoute
  ) {}

  @ViewChild(MatSort)
  sort!: MatSort;
  ngOnInit(): void {
    this.classId = this.route.snapshot.params['id'];
    this.pageload();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  rowClassRules = {
    'green-row': function (params: any) {
      return params.data.isComplete;
    },
    'yellow-row': function (params: any) {
      return !params.data.isComplete;
    },
  };

  pageload() {
    this.classId = this.route.snapshot.params['id'];
    this.apiBody = { ClassId: this.classId };
    this.monthService.quarterlyMarketList(this.apiBody).subscribe((data) => {
      // this.monthList = data;
      this.$rows = data;
      // console.log(this.monthList[0]);
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

          if (this.currentQuarter != 0) {
            this.QuarterNoLabel = String(Number(this.currentQuarter) + 1);
            // ifComplete = Convert.ToBoolean(quarterAdapter.ScalarQueryIfCompleted((Guid)Session["session"], currentQuarter));
            if (this.isMonthCompleted == false) {
              this.isNewQuarterButtonDisable = true;
              this.MessageLabel =
                'Month ' +
                this.currentQuarter +
                " hasn't finished. You can't create new month at this moment.";
            } else {
              this.isNewQuarterButtonDisable = false;
              this.MessageLabel =
                'Month ' +
                this.currentQuarter +
                ' has been finished. You can create new month at this moment.';
            }
          } else if (this.currentQuarter == 0) {
            this.isNewQuarterButtonDisable = false;
            this.MessageLabel = 'No month has been created.';
          }
          if (this.classInfo.status != 'T') {
            this.isFinalizeButtonDisable = true;
          } else {
            this.isFinalizeButtonDisable = false;
          }
          ////"C" means that calucation is not finished yet.
          if (this.classInfo.status == 'C') {
            this.isFinalizeButtonDisable = true;
            this.isNewQuarterButtonDisable = true;
          }
        });
    });
  }

  monthCalculation() {
    this.apiBody = {
      ClassId: this.route.snapshot.params['id'],
    };
    this.monthCalculationService
      .monthCalculate(this.apiBody)
      .subscribe((data) => {
        this.dataSourceMonth.data = data;
        this.pageload();
        this.snackBar.open('Calculate successfully', 'close', {
          duration: 3000,
        });
      });
  }

  CreateNewMonth() {
    this.btnCreateNewMonth = 'Processing.......';
    this.isError = false;
    this.errorMsg = '';

    if (this.MarketTextBox == null || this.MarketTextBox.length == 0) {
      this.isError = true;
      this.errorMsg = 'Required Field';
      this.btnCreateNewMonth = 'Create a New Month';
      return;
    }

    this.isNewQuarterButtonDisable = true;
    this.apiBody = { ClassId: this.classId, TotalMarket: this.MarketTextBox };
    this.monthService.createNewMonth(this.apiBody).subscribe((data) => {
      this.isError = false;
      this.errorMsg = '';
      this.pageload();
      this.btnCreateNewMonth = 'Create a New Month';
      this.snackBar.open('Create a New Month successfully', 'close', {
        duration: 3000,
      });
      //console.log(data.Data.monthID);
    });
  }

  FinalizeMonth() {
    this.btnfinltext = 'Processing.......';
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
          this.pageload();
          this.btnfinltext = 'Finalize Now';
          this.snackBar.open('Finalize successfully', 'close', {
            duration: 3000,
          });
          //console.log(data.message);
          //console.log(data.Data.monthID);
        });
      });
  }
}
