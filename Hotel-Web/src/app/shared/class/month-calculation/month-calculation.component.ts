import { Component, ViewChild } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { MonthDto } from './month-calculation.model';
import { MatRow, MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MonthCalculationService } from './monthcalculation.service';
@Component({
  selector: 'app-month-calculation',
  templateUrl: './month-calculation.component.html',
  styleUrls: ['./month-calculation.component.css'],
})
export class MonthCalculationComponent {
  $rows: MonthDto[] = [];
  // monthList: MonthDto[] = [];
  monthInfo: MonthDto = {
    monthId: '',
    classId: '',
    sequence: '',
    totalMarket: '',
    isComplete: false,
    configId: '',
    status: '',
  };
  apiBody = {};
  btnCalculationText: string = 'Calculate Now';
  classId: number | undefined;
  dataSource = new MatTableDataSource<MonthDto>();
  dataSourceMonth = new MatTableDataSource<MonthDto>();
  //classId: any;
  monthId: number = 0;
  columnDefs: ColDef[] = [
    {
      field: 'monthId',
    },

    {
      field: 'status',
    },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    sortable: true,
  };
  constructor(
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
  pageload() {
    this.classId = this.route.snapshot.params['id'];
    this.apiBody = {
      ClassId: this.classId,
      MonthId: this.monthId,
    };
    this.monthCalculationService.monthList(this.apiBody).subscribe((data) => {
      // this.monthList = data;
      this.$rows = data.data;
      this.dataSourceMonth.data = data;
    });
  }
  monthCalculation() {
    this.btnCalculationText = 'Processing.....';
    this.apiBody = {
      ClassId: this.classId,
    };
    this.monthCalculationService
      .monthCalculate(this.apiBody)
      .subscribe((data) => {
        // this.monthList = data;
        this.dataSourceMonth.data = data;
        this.pageload();
        this.btnCalculationText = 'Calculate Now';
        this.snackBar.open('Calculate successfully', 'close', {
          duration: 3000,
        });
        this.router.navigate(['../create-month']);
      });
  }
}
