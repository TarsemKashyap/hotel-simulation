import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService, InstructorSignup } from 'src/app/public/account';
import { ClassSession } from '..';
import { ClassService } from '../class.service';

@Component({
  selector: 'app-add-class',
  templateUrl: './add-class.component.html',
  styleUrls: ['./add-class.component.css'],
})
export class AddClassComponent {
  form: FormGroup;
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    private _snackBar: MatSnackBar
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {}

  private createForm(): FormGroup {
    return this.fb.group({
      title: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      hotelsCount: [500, Validators.required],
      roomInEachHotel: [4, Validators.required],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const sigup: ClassSession = {
      title: this.form.value.title,
      startDate: this.form.value.startDate,
      endDate: this.form.value.endDate,
      hotelsCount: this.form.value.hotelsCount,
      roomInEachHotel: this.form.value.roomInEachHotel,
      currentQuater: this.form.value.currentQuater,
      code: this.form.value.code,
    };
    this.classService.addClass(sigup).subscribe((x) => {
      console.log('Signup', x);
      this._snackBar.open('Instructor Account created');
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
