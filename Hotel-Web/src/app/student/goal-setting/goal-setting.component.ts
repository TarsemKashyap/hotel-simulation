import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { StudentService } from '../student.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-goal-setting',
  templateUrl: './goal-setting.component.html',
  styleUrls: ['./goal-setting.component.css']
})
export class GoalSettingComponent {
  form: FormGroup;
  submitted = false;
  errorMsg: string = "";

  constructor(
    private studentService: StudentService, private fb: FormBuilder, private _snackBar: MatSnackBar) {
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
    //   Advertising1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Advertising8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   SalesForce8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   Promotions8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   PublicRelations8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborAdvertising8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborSalesForce8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPromotions8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    //   LaborPublicRelations8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
     });
  }
}
