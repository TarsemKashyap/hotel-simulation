import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddClassComponent } from './add-class/add-class.component';
import { ClassService } from './class.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [AddClassComponent],
  imports: [CommonModule,ReactiveFormsModule],
  providers: [ClassService],
})
export class ClassModule {}
