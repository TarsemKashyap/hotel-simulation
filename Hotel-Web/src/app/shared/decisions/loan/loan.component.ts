import { Component, Injector, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { StudentService } from '../../../student/student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DecimalValidator } from 'src/app/shared/class/model/classSession.model';
import { BaseDecision } from '../decision/decision.component';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css'],
})
export class LoanComponent extends BaseDecision implements OnInit {
  form: FormGroup;
  submitted = false;
  errorMsg: string = '';
  balanceSheetDetails: any;
  longTermLoan: number = 0;
  emergencyLoan: number = 0;
  isBind: boolean = false;

  constructor(
    private studentService: StudentService,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    injector: Injector
  ) {
    super(injector);
    this.form = this.createForm();
  }

  ngOnInit(): void {
    this.longTermLoan = 0;
    this.getBalanceSheetDetail();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private async getBalanceSheetDetail() {
    let defaultClass = await this.getActiveClass();

    this.studentService.BalanceSheetDetails(defaultClass).subscribe((data) => {
      this.balanceSheetDetails = data || {};
      this.form.patchValue({
        payLongTermLoan: this.balanceSheetDetails.longDebtPay,
        payEmergencyLoan: this.balanceSheetDetails.shortDebtPay,
      });
      this.longTermLoan = this.balanceSheetDetails.longDebt;
      this.emergencyLoan = this.balanceSheetDetails.shortDebt;
      this.isBind = true;
    });
  }

  payLongCustomValidator() {
    return (control: FormControl) => {
      if (this.isBind) {
        if (this.longTermLoan < parseFloat(control.value)) {
          return { Invalidvalue: true };
        } else {
          return null;
        }
      }
      return null;
    };
  }

  payEmergencyCustomValidator() {
    return (control: FormControl) => {
      if (this.isBind) {
        if (this.emergencyLoan != undefined) {
          if (this.emergencyLoan < parseFloat(control.value)) {
            return { Invalidvalue: true };
          } else {
            return null;
          }
        }
      }
      return null;
    };
  }

  private createForm(): FormGroup {
    return this.fb.group({
      payLongTermLoan: [
        '',
        [Validators.required, DecimalValidator, this.payLongCustomValidator()],
      ],
      payEmergencyLoan: [
        '',
        [
          Validators.required,
          DecimalValidator,
          this.payEmergencyCustomValidator(),
        ],
      ],
      borrowLongTermLoan: ['', [Validators.required, DecimalValidator]],
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }
    this.balanceSheetDetails.longDebtPay = parseFloat(
      this.form.value.payLongTermLoan
    );
    this.balanceSheetDetails.shortDebtPay = parseFloat(
      this.form.value.payEmergencyLoan
    );
    this.balanceSheetDetails.longBorrow = parseFloat(
      this.form.value.borrowLongTermLoan
    );
    this.studentService
      .UpdateBalanceSheetDetails(this.balanceSheetDetails)
      .subscribe((x) => {
        this._snackBar.open('Balance Sheet updated successfully', 'Close', {
          duration: 3000,
        });
        this.getBalanceSheetDetail();
      });
  }
}
