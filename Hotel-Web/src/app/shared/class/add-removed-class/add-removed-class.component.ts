import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { ColDef, IRowNode } from 'ag-grid-community';
import { Utility } from '../../utility';
import { ClassMapping, ClassSession } from '../model/classSession.model';
import { ClassService } from '../class.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-removed-class',
  templateUrl: './add-removed-class.component.html',
  styleUrls: ['./add-removed-class.component.css']
})
export class AddRemovedClassComponent {
  private datePipe = new DatePipe('en-US');
  isDefault : any;
  Titles : ClassSession [] = [];
  selectedTitle : ClassSession | undefined;
  myForm!: FormGroup;
  columnDefs: ColDef[] = [
    {
      field: 'code',
      tooltipValueGetter:()=>"Click to copy code",
      onCellClicked: (event) => {
        Utility.copyToClipboard(event.value);
        console.log("code",event.value)
        this.snackBar.open(`class code ${event.value} copied.`)
      },
    },
    { field: 'title' },
    {
      field: 'startDate',
      cellRenderer: (params: { value: string | number | Date }) =>
        this.datePipe.transform(params.value, 'dd-MM-yyyy'),
    },
    {
      field: 'endDate',
      cellRenderer: (params: { value: string | number | Date }) =>
        this.datePipe.transform(params.value, 'dd-MM-yyyy'),
    },
    { field: 'createdBy' },
    {
      field: 'isDefault',
      headerName: 'Select as Default',
      onCellClicked: (event) => {
        this.isDefault = true;
        this.classService.isDefaultUpdate({isDefault: this.isDefault }).subscribe((data) => {
          this.snackBar.open('isDefault set succesfully')
        })
      }
      
    },
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 150,
    filter: 'agTextColumnFilter',
    menuTabs: ['filterMenuTab'],
    sortable: true,
  };

  $rows: ClassSession[] = [];

  constructor(
    private classService: ClassService,
    private router: Router,
    public snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
  ) {
    this.myForm = this.formBuilder.group({
      selectedTitle: "",
    });
  }

  ngOnInit(): void {
    this.classService.Instructorlist().subscribe((x) => {
      this.$rows = x;
    });
    this.studentRoles();
  }

  private studentRoles() {
    this.classService
      .ClassTitlelist()
      .subscribe((data) => {
      this.Titles = data;
      });
  }

  Save() {
    debugger
    const title = this.selectedTitle?.title;
    const studentAssignRoles: ClassMapping = {
      title: title!
    };
    this.classService.SaveClass(studentAssignRoles).subscribe((response) => {
      this.snackBar.open("Student Assign Class Saved Successfully")
    });
  }
}
