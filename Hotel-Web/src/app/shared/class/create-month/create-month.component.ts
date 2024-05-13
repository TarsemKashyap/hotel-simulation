import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
//import { MonthService } from './month.service';
//import { MonthDto } from './month.model';
import { MonthService } from './month.service';

import { ClassStatus, MonthDto } from './month.model';
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
import { OverlayService } from '../../overlay.service';
import { finalize } from 'rxjs';

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
  currentQuarter: number = 0;
  MarketTextBox: string = '';
  btnfinltext: string = 'Finalize Now';
  btnCreateNewMonth: string = 'Create a New Month';
  apiBody = {};
  disableCalcBtn: boolean = true;
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
    { field: 'statusText', headerName: 'Status' },
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
    public route: ActivatedRoute,
    private overlayService: OverlayService
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
      this.$rows = data;
      this.dataSourceMonth.data = data;
    });

    this.monthService.classInfo(this.classId).subscribe((data) => {
      this.classInfo = data;
      this.currentQuarter = parseInt(this.classInfo.currentQuater);
      console.log({ classInfo: data });

      this.isFinalizeButtonDisable = this.classInfo.status != ClassStatus.T;

      //"C" means that calucation is not finished yet.
      if (this.classInfo.status == ClassStatus.C) {
        this.isFinalizeButtonDisable = true;
        this.isNewQuarterButtonDisable = true;
      }
      this.monthService
        .monthInfo(this.classId, this.currentQuarter)
        .subscribe((data) => {
          if (data) {
            this.monthInfo = data;
            this.isMonthCompleted = this.monthInfo.isComplete;
            this.disableCalcBtn = this.isCalcbtnDisable(
              data,
              this.currentQuarter
            );
          }

          if (this.currentQuarter == 0) {
            this.isNewQuarterButtonDisable = false;
            this.MessageLabel = 'No month has been created.';
          } else if (this.currentQuarter != 0) {
            this.QuarterNoLabel = String(Number(this.currentQuarter) + 1);
            // ifComplete = Convert.ToBoolean(quarterAdapter.ScalarQueryIfCompleted((Guid)Session["session"], currentQuarter));
            this.isNewQuarterButtonDisable = !this.isMonthCompleted;
            if (this.isMonthCompleted == false) {
              this.MessageLabel = `Month ${this.currentQuarter} hasn't finished. You can't create new month at this moment.`;
            } else {
              this.MessageLabel = `Month ${this.currentQuarter} has been finished. You can create new month at this moment.`;
            }
          }
        });
    });
  }

  isCalcbtnDisable(month: MonthDto, querter: number): boolean {
    if (querter == 0) {
      return true;
    }
    switch (month.status) {
      case ClassStatus.T:
      case ClassStatus.S:
      case ClassStatus.A:
      case ClassStatus.C:
        return true;
      case ClassStatus.I:
        return false;
      default:
        return false;
    }
  }

  monthCalculation() {
    this.apiBody = {
      ClassId: this.route.snapshot.params['id'],
    };
    // const dialog = this.overlayService.open('Calculating month');
    const obs = this.monthCalculationService.monthCalculate(this.apiBody);
    this.overlayService.loader('Calculating month', obs).subscribe((data) => {
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
    const dialog = this.overlayService.open('Please wait, Creating new month');

    this.isNewQuarterButtonDisable = true;
    this.apiBody = { ClassId: this.classId, TotalMarket: this.MarketTextBox };
    this.monthService
      .createNewMonth(this.apiBody)
      .pipe(finalize(() => dialog.close()))
      .subscribe((data) => {
        this.isError = false;
        dialog.close();
        this.errorMsg = '';
        this.pageload();
        this.btnCreateNewMonth = 'Create a New Month';
        this.snackBar.open('Create a New Month successfully', 'close', {
          duration: 3000,
        });
      });
  }

  FinalizeMonth() {
    this.btnfinltext = 'Processing.......';
    this.apiBody = {
      ClassId: this.classId,
      Sequence: this.currentQuarter,
      IsComplete: true,
    };
    const dialog = this.overlayService.open('Finalizing month');

    this.monthService
      .updateMonthCompletedStatus(this.apiBody)
      .subscribe((data) => {
        console.log('isCompletedDone:=' + data);
        //console.log(data.message);
        //console.log(data.Data.monthID);
        this.apiBody = { ClassId: this.classId, Status: 'A' };
        this.monthService
          .UpdateClassStatus(this.apiBody)
          .pipe(finalize(() => dialog.close()))

          .subscribe((data) => {
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
