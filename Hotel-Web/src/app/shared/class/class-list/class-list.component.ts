import { Component, OnInit } from '@angular/core';
import { ClassService } from '../class.service';
import { ColDef } from 'ag-grid-community';
import { ClassSession } from '../model/classSession.model';
import { ActionRendererComponent } from '../action-renderer/action-renderer.component';

@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css'],
})
export class ClassListComponent implements OnInit {
  columnDefs: ColDef[] = [
    { field: 'code' },
    { field: 'title' },
    { field: 'startDate'},
    { field: 'endDate' },
    { field: 'createdBy' },
    { field: 'createdOn' },
    { field: 'action', cellRenderer: ActionRendererComponent }
  ];
 defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    menuTabs: ['filterMenuTab'],
    sortable:true
  };
  $rows: ClassSession[] = [];
  constructor(private classService: ClassService) {}

  ngOnInit(): void {
    this.classService.list().subscribe((x) => {
      this.$rows = x;
    });
  }
}
