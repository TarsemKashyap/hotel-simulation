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
import { CreateMonthComponent } from './create-month/create-month.component';
import { MaterialModule } from 'src/app/material.module';
import { ClassMenuComponent } from './class-menu/class-menu.component';

@NgModule({
  declarations: [
    AddClassComponent,
    ClassListComponent,
    ClassEditComponent,
    StudentListComponent,
    StudentRolesEditComponent,
    GridActionComponent,
    AddRemovedClassComponent,
    CreateMonthComponent,
    ClassMenuComponent,
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
    CommonModule,
    ReactiveFormsModule,
    AgGridModule,
    FormsModule,
    MaterialModule,
  ],
  providers: [ClassService],
})
export class ClassModule {}

export const classRoute: Routes = [
  {
    path: 'class',
    children: [
      { path: 'add', component: AddClassComponent },
      { path: ':id/edit', component: ClassEditComponent },
      { path: 'list', component: ClassListComponent },
      { path: ':id/student-list', component: StudentListComponent },
      { path: ':id/create-month', component: CreateMonthComponent },
      {
        path: 'student-roles-edit/:id',
        component: StudentRolesEditComponent,
      },
    ],
  },
];
