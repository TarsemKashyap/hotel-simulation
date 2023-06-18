import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClassComponent } from './add-class/add-class.component';
import { ClassService } from './class.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { RouterModule, Routes } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';
import { ClassListComponent } from './class-list/class-list.component';
import { ClassEditComponent } from './class-edit/class-edit.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { GridActionComponent } from './grid-action/grid-action.component';
import { StudentListComponent } from './student-list/student-list.component';
import { MatTableModule } from '@angular/material/table';
import { StudentRolesEditComponent } from './student-roles-edit/student-roles-edit.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { AddRemovedClassComponent } from './add-removed-class/add-removed-class.component';

@NgModule({
  declarations: [
    AddClassComponent,
    ClassListComponent,
    ClassEditComponent,
    StudentListComponent,
    StudentRolesEditComponent,
    GridActionComponent,
    AddRemovedClassComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    AgGridModule,
    MatIconModule,
    MatMenuModule,
    MatTableModule,
    MatDialogModule,
    MatSelectModule,
    FormsModule,
    RouterModule,
  ],
  providers: [ClassService],
})
export class ClassModule {}

export const classRoute: Routes = [
  { path: 'class/add', component: AddClassComponent },
  { path: 'class/edit/:id', component: ClassEditComponent },
  { path: 'class/list', component: ClassListComponent },
  { path: 'class/student-list/:id', component: StudentListComponent },
  {
    path: 'class/student-roles-edit/:id',
    component: StudentRolesEditComponent,
  },
];
