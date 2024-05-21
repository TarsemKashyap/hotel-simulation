import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ClassSession } from '..';
import { ClassService } from '../class.service';
import { ClassGroup } from '../model/classSession.model';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Observable, Subject } from 'rxjs';
import { group } from '@angular/animations';

@Component({
  selector: 'app-add-class',
  templateUrl: './add-class.component.html',
  styleUrls: ['./add-class.component.css'],
})
export class AddClassComponent {
  form: FormGroup;
  submitted = false;
  classCode = null || '';
  hotelCount: number = 4;

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {}

  private createForm(): FormGroup {
    return this.fb.group({
      title: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
     // hotelsCount: [{ value: this.hotelCount }, Validators.required],
      roomInEachHotel: [{ value: 500, disabled: true }, Validators.required],
      groups: this.fb.array(this.createGroups()),
    });
  }

  createGroups() {
    const groups: FormGroup[] = [];
    for (let index = 0; index < this.hotelCount; index++) {
      let hotelGroup = new FormGroup({
        name: new FormControl(`Hotel ${index + 1}`, Validators.required),
      });
      groups.push(hotelGroup);
    }
    return groups;
  }

  get groups(): FormArray {
    return <FormArray>this.form.get('groups');
  }

  createItem(initValue: string = '') {
    return new FormGroup({
      name: new FormControl(initValue, Validators.required),
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  addGroup($event: Event, index: number): void {
    const val = `Hotel ${this.groups.length + 1}`;
    this.hotelCount += 1;
    this.groups.push(this.createItem(val));
  }

  removeGroup($event: Event, index: number): void {
    this.groups.removeAt(index);
    this.hotelCount -= 1;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const groups = (<Array<any>>this.form.value.groups).map((x, i) => {
      var data: ClassGroup = {
        serial: i + 1,
        name: x.name,
        balance: 1,
        action: 1,
      };
      return data;
    });
    const sigup: ClassSession = {
      title: this.form.value.title,
      startDate: this.form.value.startDate,
      endDate: this.form.value.endDate,
      hotelsCount: groups.length,
      roomInEachHotel: 500,
      currentQuater: 0, //this.form.value.currentQuater,
      code: this.form.value.code,
      groups: groups,
      createdBy: '',
    };
    this.classService.addClass(sigup).subscribe((x) => {
      this.classCode = x.code;
      this.router.navigate(['admin/class', 'list']);
      this._snackBar.open('Class Created successfully');
      // this._snackBar.open(
      //   `Class has been created. Class Code is ${x.code}`,
      //   '',
      //   {
      //     horizontalPosition: 'right',
      //     verticalPosition: 'top',
      //   }
      // );
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
