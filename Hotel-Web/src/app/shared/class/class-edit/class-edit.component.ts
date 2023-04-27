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
  data: ClassSession | undefined;

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
      groups: this.fb.array([])
    });
  }


  createItem(value: ClassGroup) {
    return new FormGroup({
      name: new FormControl(value.name, Validators.required),
      groupId: new FormControl(value.groupId, Validators.required),
      serial: new FormControl(value.serial, Validators.required),
      action: new FormControl(value.action)
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
   
    const groups = <FormArray>this.form.get('groups');
    console.log("GroupControls", groups.controls.map(x => x.value));
    return groups;
  }



  addGroup($event: Event, index: number): void {
    const data: ClassGroup = { name: "", serial: this.data!.groups.length + 1, balance: 0, action: Action.Added };
    this.groups.push(this.createItem(data));
    this.data!.groups.push(data);

  }

  removeGroup($event: Event, index: number): void {
    this.data!.groups[index].action = Action.Removed;
    this.groups.patchValue(this.data!.groups.filter(x=>x.action!=Action.Removed));
  }

  private loadClass() {
    this.classService.getClass(this.classId!).subscribe({
      next: (data: ClassSession) => {
        this.data = data;
        this.form.patchValue(this.data);
        for (let item of this.data.groups) {
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
    const groups = (<Array<any>>this.form.value.groups).map((x, i) => {
      var data: ClassGroup = { serial: i + 1, name: x.name, balance: 1, action: x.action };
      return data;
    });
    this.classId = this.route.snapshot.params['id'];
    const updateClass: ClassSession = {
      title: this.form.value.title,
      startDate: this.form.value.startDate,
      endDate: this.form.value.endDate,
      hotelsCount: this.form.value.hotelsCount,
      roomInEachHotel: this.form.value.roomInEachHotel,
      currentQuater: this.form.value.currentQuater,
      code: this.form.value.code && this.form.value.code == undefined && this.form.value.code == null ? this.form.value.code : '',
      createdBy: '',
      groups: groups
    };
    this.classService.classUpdate(this.classId!, updateClass).subscribe((x) => {
      this.router.navigate(['admin/class', 'list']);
      this._snackBar.open('Class successfully updated');
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
