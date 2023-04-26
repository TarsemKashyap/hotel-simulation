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
import { ClassGroup } from '../model/classSession.model';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-class-edit',
  templateUrl: './class-edit.component.html',
  styleUrls: ['./class-edit.component.css']
})
export class ClassEditComponent {
  form: FormGroup;
  submitted = false;
  classCode = null || '';
  classId: number | undefined;

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
      added: this.fb.array([this.createItem()]),
      removed: this.fb.array([this.createItem()]),
      groups: this.fb.array([this.createItem()])
    });
  }


  createItem() {
    return new FormGroup({
      name: new FormControl('', Validators.required),
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  get added(): FormArray {
    return <FormArray>this.form.get('added');
  }

  get removed(): FormArray {
    return <FormArray>this.form.get('removed');
  }

  get groups(): FormArray {
    return <FormArray>this.form.get('groups');
  }
  addGroup($event: Event, index: number): void {
    this.added.push(this.createItem());
  }
  
  removeGroup($event: Event, index: number): void {
    this.removed.removeAt(index);
  }

  private loadClass() {
    this.classService.getClass(this.classId!).subscribe({
      next: (data: ClassSession) => {
        this.form.patchValue(data);
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
    this.classId = this.route.snapshot.params['id'];
    const updateClass: ClassSession = {
      title: this.form.value.title,
      startDate: this.form.value.startDate,
      endDate: this.form.value.endDate,
      hotelsCount: this.form.value.hotelsCount,
      roomInEachHotel: this.form.value.roomInEachHotel,
      currentQuater: this.form.value.currentQuater,
      code: this.form.value.code,
      createdBy: '',
      groups: []
    };
    this.classService.classUpdate(this.classId!, updateClass).subscribe((x) => {
      this.router.navigate(['admin/class','list']);
      this._snackBar.open('Class successfully updated');
    });
  }
}
