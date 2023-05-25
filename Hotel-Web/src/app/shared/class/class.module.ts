import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClassComponent } from './add-class/add-class.component';
import { ClassService } from './class.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Routes } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';
import { ClassListComponent } from './class-list/class-list.component';
import { ActionRendererComponent } from './action-renderer/action-renderer.component';
import { ClassEditComponent } from './class-edit/class-edit.component';
import { MatIconModule } from '@angular/material/icon';
import { GridActionComponent } from './grid-action/grid-action.component';

@NgModule({
  declarations: [AddClassComponent, ClassListComponent, ActionRendererComponent, GridActionComponent, ClassEditComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    AgGridModule,
    MatIconModule
  ],
  providers: [ClassService],
})
export class ClassModule {}

export const classRoute: Routes = [
  { path: 'class/add', component: AddClassComponent },
  { path: 'class/edit/:id', component: ClassEditComponent },
  { path: 'class/list', component: ClassListComponent },
];
