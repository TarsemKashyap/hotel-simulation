import { Component } from '@angular/core';

import { StudentService } from '../student.service';

import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DecimalValidator, RoomAllocations } from 'src/app/shared/class/model/classSession.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent {
  form: FormGroup;
  submitted = false;
  WeekdayTotal:number = 0;
  WeekendTotal:number = 0;
  roomAllowWeekdays:number = 0;
  roomAllowWeekend:number = 0;
  errorMsg:string = "";
  roomAllocations:RoomAllocations[] = [];

  ngOnInit(): void {
    this.roomAllocationList();
  }

  constructor(
    private studentService: StudentService, private fb: FormBuilder,private _snackBar: MatSnackBar) {
      this.form = this.createForm();
  }

  private roomAllocationList() {
    this.studentService.RoomAllocationList().subscribe((data) => {
     
        this.roomAllocations =  data;
       
       var weekday1RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Business' && d.weekday == true)?.roomsAllocated;
       weekday1RoomAllocated = weekday1RoomAllocated == undefined ? 0 : weekday1RoomAllocated / 17;

       var weekend1RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Business' && d.weekday == false)?.roomsAllocated;
       weekend1RoomAllocated = weekend1RoomAllocated == undefined ? 0 : weekend1RoomAllocated / 13;

       var weekday2RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Small Business' && d.weekday == true)?.roomsAllocated;
       weekday2RoomAllocated = weekday2RoomAllocated == undefined ? 0 : weekday2RoomAllocated / 17;

       var weekend2RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Small Business' && d.weekday == false)?.roomsAllocated;
       weekend2RoomAllocated = weekend2RoomAllocated == undefined ? 0 : weekend2RoomAllocated / 13;

       var weekday3RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Corporate contract' && d.weekday == true)?.roomsAllocated;
       weekday3RoomAllocated = weekday3RoomAllocated == undefined ? 0 : weekday3RoomAllocated / 17;

       var weekend3RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Corporate contract' && d.weekday == false)?.roomsAllocated;
       weekend3RoomAllocated = weekend3RoomAllocated == undefined ? 0 : weekend3RoomAllocated / 13;

       var weekday4RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Families' && d.weekday == true)?.roomsAllocated;
       weekday4RoomAllocated = weekday4RoomAllocated == undefined ? 0 : weekday4RoomAllocated / 17;

       var weekend4RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Families' && d.weekday == false)?.roomsAllocated;
       weekend4RoomAllocated = weekend4RoomAllocated == undefined ? 0 : weekend4RoomAllocated / 13;

       var weekday5RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Afluent Mature Travelers' && d.weekday == true)?.roomsAllocated;
       weekday5RoomAllocated = weekday5RoomAllocated == undefined ? 0 : weekday5RoomAllocated / 17;

       var weekend5RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Afluent Mature Travelers' && d.weekday == false)?.roomsAllocated;
       weekend5RoomAllocated = weekend5RoomAllocated == undefined ? 0 : weekend5RoomAllocated / 13;

       var weekday6RoomAllocated =  this.roomAllocations.find(d => d.segment === 'International leisure travelers' && d.weekday == true)?.roomsAllocated;
       weekday6RoomAllocated = weekday6RoomAllocated == undefined ? 0 : weekday6RoomAllocated / 17;

       var weekend6RoomAllocated =  this.roomAllocations.find(d => d.segment === 'International leisure travelers' && d.weekday == false)?.roomsAllocated;
       weekend6RoomAllocated = weekend6RoomAllocated == undefined ? 0 : weekend6RoomAllocated / 13;

       var weekday7RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Corporate/Business Meetings' && d.weekday == true)?.roomsAllocated;
       weekday7RoomAllocated = weekday7RoomAllocated == undefined ? 0 : weekday7RoomAllocated / 17;

       var weekend7RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Corporate/Business Meetings' && d.weekday == false)?.roomsAllocated;
       weekend7RoomAllocated = weekend7RoomAllocated == undefined ? 0 : weekend7RoomAllocated / 13;

       var weekday8RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Association Meetings' && d.weekday == true)?.roomsAllocated;
       weekday8RoomAllocated = weekday8RoomAllocated == undefined ? 0 : weekday8RoomAllocated / 17;

       var weekend8RoomAllocated =  this.roomAllocations.find(d => d.segment === 'Association Meetings' && d.weekday == false)?.roomsAllocated;
       weekend8RoomAllocated = weekend8RoomAllocated == undefined ? 0 : weekend8RoomAllocated / 13;

       this.form.patchValue({weekDay1: weekday1RoomAllocated,Weekend1 : weekend1RoomAllocated,weekDay2: weekday2RoomAllocated,Weekend2 : weekend2RoomAllocated , weekDay3: weekday3RoomAllocated,Weekend3 : weekend3RoomAllocated , weekDay4: weekday4RoomAllocated,Weekend4 : weekend4RoomAllocated , weekDay5: weekday5RoomAllocated,Weekend5 : weekend5RoomAllocated , weekDay6: weekday6RoomAllocated,Weekend6 : weekend6RoomAllocated , weekDay7: weekday7RoomAllocated,Weekend7 : weekend7RoomAllocated , weekDay8: weekday8RoomAllocated,Weekend8 : weekend8RoomAllocated});
      
    this.sum();
    var sumTheAlloW = 0;
    var sumTheAlloE = 0;
    sumTheAlloW = parseInt(this.form.value.weekDay1 === '' ? 0 : this.form.value.weekDay1);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay2 === '' ? 0 : this.form.value.weekDay2);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay3 === '' ? 0 : this.form.value.weekDay3);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay4 === '' ? 0 : this.form.value.weekDay4);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay5 === '' ? 0 : this.form.value.weekDay5);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay6 === '' ? 0 : this.form.value.weekDay6);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay7 === '' ? 0 : this.form.value.weekDay7);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay8 === '' ? 0 : this.form.value.weekDay8);

    sumTheAlloE = parseInt(this.form.value.Weekend1 === '' ? 0 : this.form.value.Weekend1);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend2 === '' ? 0 : this.form.value.Weekend2);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend3 === '' ? 0 : this.form.value.Weekend3);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend4 === '' ? 0 : this.form.value.Weekend4);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend5 === '' ? 0 : this.form.value.Weekend5);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend6 === '' ? 0 : this.form.value.Weekend6);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend7 === '' ? 0 : this.form.value.Weekend7);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend8 === '' ? 0 : this.form.value.Weekend8);
    },
    (error) => {                              
      console.error('error caught in component',error.error);
      this._snackBar.open(error.error);
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    
    var sumTheAlloW = 0;
    var sumTheAlloE = 0;
    sumTheAlloW = parseInt(this.form.value.weekDay1 === '' ? 0 : this.form.value.weekDay1);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay2 === '' ? 0 : this.form.value.weekDay2);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay3 === '' ? 0 : this.form.value.weekDay3);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay4 === '' ? 0 : this.form.value.weekDay4);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay5 === '' ? 0 : this.form.value.weekDay5);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay6 === '' ? 0 : this.form.value.weekDay6);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay7 === '' ? 0 : this.form.value.weekDay7);
    sumTheAlloW = sumTheAlloW + parseInt(this.form.value.weekDay8 === '' ? 0 : this.form.value.weekDay8);

    sumTheAlloE = parseInt(this.form.value.Weekend1 === '' ? 0 : this.form.value.Weekend1);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend2 === '' ? 0 : this.form.value.Weekend2);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend3 === '' ? 0 : this.form.value.Weekend3);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend4 === '' ? 0 : this.form.value.Weekend4);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend5 === '' ? 0 : this.form.value.Weekend5);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend6 === '' ? 0 : this.form.value.Weekend6);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend7 === '' ? 0 : this.form.value.Weekend7);
    sumTheAlloE = sumTheAlloE + parseInt(this.form.value.Weekend8 === '' ? 0 : this.form.value.Weekend8);

    if (sumTheAlloW > 500 || sumTheAlloE > 500)
    {
        this.errorMsg = "The number of rooms you just allocated exceeded the maximum number allow!";
        this.roomAllowWeekdays = sumTheAlloW;
        this.roomAllowWeekend = sumTheAlloE;
    } else {
      this.errorMsg = "";
      this.roomAllocations.forEach(element => {
        if (element.segment == 'Business' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay1) * 17;
        if (element.segment == 'Business' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend1) * 13;

        if (element.segment == 'Small Business' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay2) * 17;
        if (element.segment == 'Small Business' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend2) * 13;

        if (element.segment == 'Corporate contract' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay3) * 17;
        if (element.segment == 'Corporate contract' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend3) * 13;

        if (element.segment == 'Families' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay4) * 17;
        if (element.segment == 'Families' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend4) * 13;

        if (element.segment == 'Afluent Mature Travelers' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay5) * 17;
        if (element.segment == 'Afluent Mature Travelers' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend5) * 13;

        if (element.segment == 'International leisure travelers' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay6) * 17;
        if (element.segment == 'International leisure travelers' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend6) * 13;

        if (element.segment == 'Corporate/Business Meetings' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay7) * 17;
        if (element.segment == 'Corporate/Business Meetings' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend7) * 13;

        if (element.segment == 'Association Meetings' && element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.weekDay8) * 17;
        if (element.segment == 'Association Meetings' && !element.weekday) 
          element.roomsAllocated = parseInt(this.form.value.Weekend8) * 13;
      });
      this.studentService.RoomAllocationUpdate(this.roomAllocations).subscribe((x) => {
       // this._snackBar.open('Instructor Account successfully updated');
      });
    }
  }

  
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  sum() {
   
      this.roomAllowWeekdays = parseInt(this.form.value.weekDay1 === '' ? 0 : this.form.value.weekDay1);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay2 === '' ? 0 : this.form.value.weekDay2);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay3 === '' ? 0 : this.form.value.weekDay3);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay4 === '' ? 0 : this.form.value.weekDay4);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay5 === '' ? 0 : this.form.value.weekDay5);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay6 === '' ? 0 : this.form.value.weekDay6);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay7 === '' ? 0 : this.form.value.weekDay7);
      this.roomAllowWeekdays = this.roomAllowWeekdays + parseInt(this.form.value.weekDay8 === '' ? 0 : this.form.value.weekDay8);

      this.roomAllowWeekend = parseInt(this.form.value.Weekend1 === '' ? 0 : this.form.value.Weekend1);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend2 === '' ? 0 : this.form.value.Weekend2);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend3 === '' ? 0 : this.form.value.Weekend3);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend4 === '' ? 0 : this.form.value.Weekend4);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend5 === '' ? 0 : this.form.value.Weekend5);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend6 === '' ? 0 : this.form.value.Weekend6);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend7 === '' ? 0 : this.form.value.Weekend7);
      this.roomAllowWeekend = this.roomAllowWeekend + parseInt(this.form.value.Weekend8 === '' ? 0 : this.form.value.Weekend8);
  }

  private createForm(): FormGroup {
    return this.fb.group({
      weekDay1: ['', [Validators.required,DecimalValidator]],
      weekDay2: ['', [Validators.required,DecimalValidator]],
      weekDay3: ['', [Validators.required,DecimalValidator]],
      weekDay4: ['', [Validators.required,DecimalValidator]],
      weekDay5: ['', [Validators.required,DecimalValidator]],
      weekDay6: ['', [Validators.required,DecimalValidator]],
      weekDay7: ['', [Validators.required,DecimalValidator]],
      weekDay8: ['', [Validators.required,DecimalValidator]],
      Weekend1: ['', [Validators.required,DecimalValidator]],
      Weekend3: ['', [Validators.required,DecimalValidator]],
      Weekend4: ['', [Validators.required,DecimalValidator]],
      Weekend5: ['', [Validators.required,DecimalValidator]],
      Weekend2: ['', [Validators.required,DecimalValidator]],
      Weekend6: ['', [Validators.required,DecimalValidator]],
      Weekend7: ['', [Validators.required,DecimalValidator]],
      Weekend8: ['', [Validators.required,DecimalValidator]]
    });
  }

}
