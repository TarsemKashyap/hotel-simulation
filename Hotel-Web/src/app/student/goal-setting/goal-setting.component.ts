import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from '../student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Goal } from 'src/app/shared/class/model/classSession.model';

@Component({
  selector: 'app-goal-setting',
  templateUrl: './goal-setting.component.html',
  styleUrls: ['./goal-setting.component.css']
})

export class GoalSettingComponent {
  form: FormGroup;
  submitted = false;
  errorMsg: string = "";
  goalDetail : any;

  ngOnInit(): void {
    this.goalDetailList();
  }

  constructor(
    private studentService: StudentService, private fb: FormBuilder, private _snackBar: MatSnackBar) {
    this.form = this.createForm();
  }

  private goalDetailList() {
    this.studentService.GoalDetails().subscribe((data) => {
      this.goalDetail = data;
        this.form.patchValue({ Occupancy: this.goalDetail.occupancyM, RoomsRevenue: this.goalDetail.roomRevenM , TotalRevenue : this.goalDetail.totalRevenM , NoOfRoomSold : this.goalDetail.shareRoomM , MarketRevenue : this.goalDetail.shareRevenM ,REVPAR :this.goalDetail.revparM , ADR : this.goalDetail.adrm ,yieldManagement : this.goalDetail.yieldMgtM , efficiencyRatio : this.goalDetail.mgtEfficiencyM , profitMargin :this.goalDetail.profitMarginM  });
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
      this.goalDetail.mgtEfficiencyM = parseFloat(this.form.value.efficiencyRatio);
      this.goalDetail.profitMarginM = parseFloat(this.form.value.profitMargin);
   
    this.studentService.UpdateGoalDetails(this.goalDetail).subscribe((x) => {
      this._snackBar.open('Goal details added successfully');
      this.goalDetailList();
    });
  }
  
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private createForm(): FormGroup {
     return this.fb.group({
      Occupancy: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      RoomsRevenue: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      TotalRevenue: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      NoOfRoomSold: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      MarketRevenue: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      REVPAR: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      ADR: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      yieldManagement: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      efficiencyRatio: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      profitMargin: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
     });
  }
}
