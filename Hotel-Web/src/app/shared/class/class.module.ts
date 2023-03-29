import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClassComponent } from './add-class/add-class.component';
import { ClassService } from './class.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Routes } from '@angular/router';

@NgModule({
  declarations: [AddClassComponent],
  imports: [CommonModule, ReactiveFormsModule, MatDatepickerModule],
  providers: [ClassService],
})
export class ClassModule {}

export const classRoute: Routes = [
  { path: 'class/add', component: AddClassComponent },
  { path: 'class/edit/:id', component: AddClassComponent },
];
