import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { StudentService } from '../student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  DecimalValidator,
  Goal,
} from 'src/app/shared/class/model/classSession.model';

@Component({
  selector: 'app-goal-setting',
  templateUrl: './goal-setting.component.html',
  styleUrls: ['./goal-setting.component.css'],
})
export class GoalSettingComponent {
  form: FormGroup;
  submitted = false;
  errorMsg: string = '';
  goalDetail: any;

  ngOnInit(): void {
    this.goalDetailList();
  }

  constructor(
    private studentService: StudentService,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar
  ) {
    this.form = this.createForm();
  }

  private goalDetailList() {
    this.studentService.GoalDetails().subscribe({
      next: (data: Goal) => {
        this.goalDetail = data;
        this.form.patchValue({
          Occupancy: this.goalDetail.occupancyM,
          RoomsRevenue: this.goalDetail.roomRevenM,
          TotalRevenue: this.goalDetail.totalRevenM,
          NoOfRoomSold: this.goalDetail.shareRoomM,
          MarketRevenue: this.goalDetail.shareRevenM,
          REVPAR: this.goalDetail.revparM,
          ADR: this.goalDetail.adrm,
          yieldManagement: this.goalDetail.yieldMgtM,
          efficiencyRatio: this.goalDetail.mgtEfficiencyM,
          profitMargin: this.goalDetail.profitMarginM,
        });
      },
      error: (err) => {
        const message=Object.values(err.error).at(0) as string;
        this._snackBar.open(message);
      },
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.goalDetail.occupancyM = parseFloat(this.form.value.Occupancy);
    this.goalDetail.roomRevenM = parseFloat(this.form.value.RoomsRevenue);
    this.goalDetail.totalRevenM = parseFloat(this.form.value.TotalRevenue);
    this.goalDetail.shareRoomM = parseFloat(this.form.value.NoOfRoomSold);
    this.goalDetail.shareRevenM = parseFloat(this.form.value.MarketRevenue);
    this.goalDetail.revparM = parseFloat(this.form.value.REVPAR);
    this.goalDetail.adrm = parseFloat(this.form.value.ADR);
    this.goalDetail.yieldMgtM = parseFloat(this.form.value.yieldManagement);
    this.goalDetail.mgtEfficiencyM = parseFloat(
      this.form.value.efficiencyRatio
    );
    this.goalDetail.profitMarginM = parseFloat(this.form.value.profitMargin);

    this.studentService.UpdateGoalDetails(this.goalDetail).subscribe((x) => {
      this._snackBar.open('Goal details updated successfully', 'Close', {
        duration: 3000,
      });
      this.goalDetailList();
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      Occupancy: ['', [Validators.required, DecimalValidator]],
      RoomsRevenue: ['', [Validators.required, DecimalValidator]],
      TotalRevenue: ['', [Validators.required, DecimalValidator]],
      NoOfRoomSold: ['', [Validators.required, DecimalValidator]],
      MarketRevenue: ['', [Validators.required, DecimalValidator]],
      REVPAR: ['', [Validators.required, DecimalValidator]],
      ADR: ['', [Validators.required, DecimalValidator]],
      yieldManagement: ['', [Validators.required, DecimalValidator]],
      efficiencyRatio: ['', [Validators.required, DecimalValidator]],
      profitMargin: ['', [Validators.required, DecimalValidator]],
    });
  }
}
