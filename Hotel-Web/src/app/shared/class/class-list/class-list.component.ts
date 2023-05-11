import { Component, OnInit } from '@angular/core';
import { ClassService } from '../class.service';
import { ColDef } from 'ag-grid-community';
import { ClassSession } from '../model/classSession.model';
import { ActionRendererComponent } from '../action-renderer/action-renderer.component';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css'],
})
export class ClassListComponent implements OnInit {
  private datePipe = new DatePipe('en-US');

  public rowData = [
    { startdate: '2023-04-26T18:30:00', enddate: '2023-04-27T18:30:00' },
    { startdate: '2023-04-28T12:00:00', enddate: '2023-04-29T12:00:00' },
  ];
  columnDefs: ColDef[] = [
    { field: 'code' },
    { field: 'title' },
    { field: 'startDate', cellRenderer: (params: { value: string | number | Date; }) =>
    this.datePipe.transform(params.value, 'dd-MM-yyyy'),},
    { field: 'endDate', cellRenderer: (params: { value: string | number | Date; }) =>
    this.datePipe.transform(params.value, 'dd-MM-yyyy'), },
    { field: 'createdBy' },
    { field: 'createdOn', cellRenderer: (params: { value: string | number | Date; }) =>
    this.datePipe.transform(params.value, 'dd-MM-yyyy'), },
    { field: 'action', cellRenderer: ActionRendererComponent,cellRendererParams:{name:'rishu', distt:['mansa,bareta']} }
  ];
 defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    menuTabs: ['filterMenuTab'],
    sortable:true
  };
  
  $rows: ClassSession[] = [];
  constructor(private classService: ClassService,
    private router: Router) {}
 
  ngOnInit(): void {
    this.classService.list().subscribe((x) => {
      this.$rows = x;
    });
  }
 

  add() {
    this.router.navigate(['class/add'])
  }
}
