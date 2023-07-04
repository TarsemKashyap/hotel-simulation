import { Component } from '@angular/core';

import { StudentService } from '../student.service';

import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent {
  form: FormGroup;
 

  submitted = false;
 

  ngOnInit(): void {

 
  }

  constructor(
    private studentService: StudentService, private fb: FormBuilder,) {
      this.form = this.createForm();
  }

  onSubmit(): void {
  
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
  }

  
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      weekDay1: ['', [Validators.required,Validators.pattern("^[0-9]*$")]],
      Weekend1: ['', Validators.required,,this.numberValidator]
    });
  }

  numberValidator(control: FormControl) {
    if (isNaN(control?.value)) {
      return {
        number: true
      }
    }
    return null;
  }



 
  private roomList() {

    this.studentService
      .RoomList().subscribe((data) => {
      
      });
  }
}
