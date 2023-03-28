import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClassComponent } from './add-class/add-class.component';
import { ClassService } from './class.service';
import { ReactiveFormsModule } from '@angular/forms';
import {MatDatepickerModule} from "@angular/material/datepicker"

@NgModule({
  declarations: [AddClassComponent],
  imports: [CommonModule,ReactiveFormsModule,MatDatepickerModule],
  providers: [ClassService],
})
export class ClassModule {}
