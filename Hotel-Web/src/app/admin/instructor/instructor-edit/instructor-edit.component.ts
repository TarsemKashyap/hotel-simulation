import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Route, Router } from '@angular/router';
import {
  AccountService,
  InstructorSignup,
  InstructorUpdate,
} from 'src/app/public/account';

@Component({
  selector: 'app-instructor-edit',
  templateUrl: './instructor-edit.component.html',
  styleUrls: ['./instructor-edit.component.css'],
})
export class InstructorEditComponent {
  form: FormGroup;
  submitted = false;
  userId: string | undefined;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params['id'];
    this.loadInstructor();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      institute: ['', Validators.required],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private loadInstructor() {
    this.accountService.getInstructor(this.userId!).subscribe({
      next: (data: InstructorUpdate) => {
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
    this.userId = this.route.snapshot.params['id'];
    const sigup: InstructorUpdate = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      email: this.form.value.email,
      institute: this.form.value.institute,
    };
    this.accountService.InstructorUpdate(this.userId!, sigup).subscribe((x) => {
      this.router.navigate(['admin/instructor','list']);
      this._snackBar.open('Instructor Account successfully updated');
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
