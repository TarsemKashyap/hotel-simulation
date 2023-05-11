import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
import { Action, ClassGroup } from '../model/classSession.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-class-edit',
  templateUrl: './class-edit.component.html',
  styleUrls: ['./class-edit.component.css'],
})
export class ClassEditComponent {
  form: FormGroup;
  submitted = false;
  classCode = null || '';
  classId: number | undefined;
  removedGroups: ClassGroup[] = [];

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {
    this.classId = this.route.snapshot.params['id'];
    this.loadClass();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      title: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      hotelsCount: [4, Validators.required],
      roomInEachHotel: [{ value: 500, disabled: true }, Validators.required],
      groups: this.fb.array([]),
    });
  }

  createItem(value: ClassGroup) {
    return new FormGroup({
      name: new FormControl(value.name, Validators.required),
      groupId: new FormControl(value.groupId),
      serial: new FormControl(value.serial, Validators.required),
      action: new FormControl(value.action || Action.Updated),
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  get groups(): FormArray {
    return <FormArray>this.form.get('groups');
  }

  addGroup($event: Event, index: number): void {
    const data: ClassGroup = {
      groupId: undefined,
      name: '',
      serial: this.groups.length + 1,
      balance: 0,
      action: Action.Added,
    };
    this.groups.push(this.createItem(data));
  }

  removeGroup($event: Event, index: number): void {
    const classGroup: ClassGroup = this.groups.controls[index].value;
    this.groups.removeAt(index);
    Object.assign(classGroup, { action: Action.Removed });
    console.log('ClassGroup removed', classGroup);
    //only add which are saved on database
    if (classGroup.groupId) {
      this.removedGroups.push(classGroup);
    }
  }

  private loadClass() {
    this.classService.getClass(this.classId!).subscribe({
      next: (data: ClassSession) => {
        this.form.patchValue(data);
        for (let item of data.groups) {
          this.groups.push(this.createItem(item));
        }
      },
      error: (err) => {
        this._snackBar.open(err.error);
      },
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const updatedGroup = (<Array<any>>this.form.value.groups).map(
      (x: ClassGroup, i) => {
        var data: ClassGroup = {
          serial: i + 1,
          name: x.name,
          balance: 1,
          action: x.action,
          groupId: x.groupId,
        };
        return data;
      }
    );
    const groups = updatedGroup.concat(this.removedGroups);
    this.classId = this.route.snapshot.params['id'];
    const updateClass: ClassSession = {
      title: this.form.value.title,
      startDate: this.form.value.startDate,
      endDate: this.form.value.endDate,
      hotelsCount: this.form.value.hotelsCount,
      roomInEachHotel: this.form.value.roomInEachHotel,
      currentQuater: this.form.value.currentQuater,
      code: '',
      createdBy: '',
      groups: groups,
    };
    this.classService.classUpdate(this.classId!, updateClass).subscribe((x) => {
      this.router.navigate(['admin/class', 'list']);
      this._snackBar.open('Class updated successfully');
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }

  studentList() {
    this.router.navigate(['class/student-list', this.classId]);
  }
}
