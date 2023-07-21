import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from '../student.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MarketingDecision } from 'src/app/shared/class/model/classSession.model';

@Component({
  selector: 'app-marketing',
  templateUrl: './marketing.component.html',
  styleUrls: ['./marketing.component.css']
})
export class MarketingComponent {
  form: FormGroup;
  submitted = false;
  errorMsg: string = "";
  totalOtherExpen: number = 0;
  totalOperatingExpen: number = 0;
  marketingDecisionList: MarketingDecision[] = [];

  ngOnInit(): void {
    this.marketDecisionList();
  }

  constructor(
    private studentService: StudentService, private fb: FormBuilder, private _snackBar: MatSnackBar) {
    this.form = this.createForm();
  }

  private marketDecisionList() {
    this.studentService.MarketingDetails().subscribe((data) => {
      this.marketingDecisionList = data;
      this.marketingDecisionList.forEach(element => {
        switch (element.marketingTechniques) {
          case "Advertising":
            switch (element.segment) {
              case "Business":
                this.form.patchValue({ Advertising1: element.spendingFormatN0, LaborAdvertising1: element.laborSpendingFormatN0 });
                break;

              case "Small Business":
                this.form.patchValue({ Advertising2: element.spendingFormatN0, LaborAdvertising2: element.laborSpendingFormatN0 });
                break;

              case "Corporate contract":
                this.form.patchValue({ Advertising3: element.spendingFormatN0, LaborAdvertising3: element.laborSpendingFormatN0 });
                break;

              case "Families":
                this.form.patchValue({ Advertising4: element.spendingFormatN0, LaborAdvertising4: element.laborSpendingFormatN0 });
                break;

              case "Afluent Mature Travelers":
                this.form.patchValue({ Advertising5: element.spendingFormatN0, LaborAdvertising5: element.laborSpendingFormatN0 });
                break;

              case "International leisure travelers":
                this.form.patchValue({ Advertising6: element.spendingFormatN0, LaborAdvertising6: element.laborSpendingFormatN0 });
                break;

              case "Corporate/Business Meetings":
                this.form.patchValue({ Advertising7: element.spendingFormatN0, LaborAdvertising7: element.laborSpendingFormatN0 });
                break;

              case "Association Meetings":
                this.form.patchValue({ Advertising8: element.spendingFormatN0, LaborAdvertising8: element.laborSpendingFormatN0 });
                break;
              default: break;
            }
            break;
          case "Sales Force":
            switch (element.segment) {
              case "Business":
                this.form.patchValue({ SalesForce1: element.spendingFormatN0, LaborSalesForce1: element.laborSpendingFormatN0 });
                break;

              case "Small Business":
                this.form.patchValue({ SalesForce2: element.spendingFormatN0, LaborSalesForce2: element.laborSpendingFormatN0 });
                break;

              case "Corporate contract":
                this.form.patchValue({ SalesForce3: element.spendingFormatN0, LaborSalesForce3: element.laborSpendingFormatN0 });
                break;

              case "Families":
                this.form.patchValue({ SalesForce4: element.spendingFormatN0, LaborSalesForce4: element.laborSpendingFormatN0 });
                break;

              case "Afluent Mature Travelers":
                this.form.patchValue({ SalesForce5: element.spendingFormatN0, LaborSalesForce5: element.laborSpendingFormatN0 });
                break;

              case "International leisure travelers":
                this.form.patchValue({ SalesForce6: element.spendingFormatN0, LaborSalesForce6: element.laborSpendingFormatN0 });
                break;

              case "Corporate/Business Meetings":
                this.form.patchValue({ SalesForce7: element.spendingFormatN0, LaborSalesForce7: element.laborSpendingFormatN0 });
                break;

              case "Association Meetings":
                this.form.patchValue({ SalesForce8: element.spendingFormatN0, LaborSalesForce8: element.laborSpendingFormatN0 });
                break;
              default: break;
            }
            break;
          case "Promotions":
            switch (element.segment) {
              case "Business":
                this.form.patchValue({ Promotions1: element.spendingFormatN0, LaborPromotions1: element.laborSpendingFormatN0 });
                break;

              case "Small Business":
                this.form.patchValue({ Promotions2: element.spendingFormatN0, LaborPromotions2: element.laborSpendingFormatN0 });
                break;

              case "Corporate contract":
                this.form.patchValue({ Promotions3: element.spendingFormatN0, LaborPromotions3: element.laborSpendingFormatN0 });
                break;

              case "Families":
                this.form.patchValue({ Promotions4: element.spendingFormatN0, LaborPromotions4: element.laborSpendingFormatN0 });
                break;

              case "Afluent Mature Travelers":
                this.form.patchValue({ Promotions5: element.spendingFormatN0, LaborPromotions5: element.laborSpendingFormatN0 });
                break;

              case "International leisure travelers":
                this.form.patchValue({ Promotions6: element.spendingFormatN0, LaborPromotions6: element.laborSpendingFormatN0 });
                break;

              case "Corporate/Business Meetings":
                this.form.patchValue({ Promotions7: element.spendingFormatN0, LaborPromotions7: element.laborSpendingFormatN0 });
                break;

              case "Association Meetings":
                this.form.patchValue({ Promotions8: element.spendingFormatN0, LaborPromotions8: element.laborSpendingFormatN0 });
                break;
              default: break;
            }
            break;
          case "Public Relations":
            switch (element.segment) {
              case "Business":
                this.form.patchValue({ PublicRelations1: element.spendingFormatN0, LaborPublicRelations1: element.laborSpendingFormatN0 });
                break;

              case "Small Business":
                this.form.patchValue({ PublicRelations2: element.spendingFormatN0, LaborPublicRelations2: element.laborSpendingFormatN0 });
                break;

              case "Corporate contract":
                this.form.patchValue({ PublicRelations3: element.spendingFormatN0, LaborPublicRelations3: element.laborSpendingFormatN0 });
                break;

              case "Families":
                this.form.patchValue({ PublicRelations4: element.spendingFormatN0, LaborPublicRelations4: element.laborSpendingFormatN0 });
                break;

              case "Afluent Mature Travelers":
                this.form.patchValue({ PublicRelations5: element.spendingFormatN0, LaborPublicRelations5: element.laborSpendingFormatN0 });
                break;

              case "International leisure travelers":
                this.form.patchValue({ PublicRelations6: element.spendingFormatN0, LaborPublicRelations6: element.laborSpendingFormatN0 });
                break;

              case "Corporate/Business Meetings":
                this.form.patchValue({ PublicRelations7: element.spendingFormatN0, LaborPublicRelations7: element.laborSpendingFormatN0 });
                break;

              case "Association Meetings":
                this.form.patchValue({ PublicRelations8: element.spendingFormatN0, LaborPublicRelations8: element.laborSpendingFormatN0 });
                break;
              default: break;
            }
            break;
          default: break;
        }
      });
      this._snackBar.open('Marketing details added successfully');
      this.marketDecisionList();
    });
  }

  sum() {
    this.totalOtherExpen = parseFloat((this.form.value.Advertising1 === undefined || this.form.value.Advertising1 === '') ? 0 : this.form.value.Advertising1.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising2 === undefined || this.form.value.Advertising2 === '') ? 0 : this.form.value.Advertising2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising3 === undefined || this.form.value.Advertising3 === '') ? 0 : this.form.value.Advertising3.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising4 === undefined || this.form.value.Advertising4 === '') ? 0 : this.form.value.Advertising4.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising5 === undefined || this.form.value.Advertising5 === '') ? 0 : this.form.value.Advertising5.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising6 === undefined || this.form.value.Advertising6 === '') ? 0 : this.form.value.Advertising6.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising7 === undefined || this.form.value.Advertising7 === '') ? 0 : this.form.value.Advertising7.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Advertising8 === undefined || this.form.value.Advertising8 === '') ? 0 : this.form.value.Advertising8.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce1 === undefined || this.form.value.SalesForce1 === '') ? 0 : this.form.value.SalesForce1.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce2 === undefined || this.form.value.SalesForce2 === '') ? 0 : this.form.value.SalesForce2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce3 === undefined || this.form.value.SalesForce3 === '') ? 0 : this.form.value.SalesForce3.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce4 === undefined || this.form.value.SalesForce4 === '') ? 0 : this.form.value.SalesForce4.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce5 === undefined || this.form.value.SalesForce5 === '') ? 0 : this.form.value.SalesForce5.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce6 === undefined || this.form.value.SalesForce6 === '') ? 0 : this.form.value.SalesForce6.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce7 === undefined || this.form.value.SalesForce7 === '') ? 0 : this.form.value.SalesForce7.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.SalesForce8 === undefined || this.form.value.SalesForce8 === '') ? 0 : this.form.value.SalesForce8.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions1 === undefined || this.form.value.Promotions1 === '') ? 0 : this.form.value.Promotions1.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions2 === undefined || this.form.value.Promotions2 === '') ? 0 : this.form.value.Promotions2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions2 === undefined || this.form.value.Promotions2 === '') ? 0 : this.form.value.Promotions2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions2 === undefined || this.form.value.Promotions2 === '') ? 0 : this.form.value.Promotions2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions3 === undefined || this.form.value.Promotions3 === '') ? 0 : this.form.value.Promotions3.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions4 === undefined || this.form.value.Promotions4 === '') ? 0 : this.form.value.Promotions4.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions5 === undefined || this.form.value.Promotions5 === '') ? 0 : this.form.value.Promotions5.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions6 === undefined || this.form.value.Promotions6 === '') ? 0 : this.form.value.Promotions6.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions7 === undefined || this.form.value.Promotions7 === '') ? 0 : this.form.value.Promotions7.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.Promotions8 === undefined || this.form.value.Promotions8 === '') ? 0 : this.form.value.Promotions8.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations1 === undefined || this.form.value.PublicRelations1 === '') ? 0 : this.form.value.PublicRelations1.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations2 === undefined || this.form.value.PublicRelations2 === '') ? 0 : this.form.value.PublicRelations2.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations3 === undefined || this.form.value.PublicRelations3 === '') ? 0 : this.form.value.PublicRelations3.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations4 === undefined || this.form.value.PublicRelations4 === '') ? 0 : this.form.value.PublicRelations4.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations5 === undefined || this.form.value.PublicRelations5 === '') ? 0 : this.form.value.PublicRelations5.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations6 === undefined || this.form.value.PublicRelations6 === '') ? 0 : this.form.value.PublicRelations6.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations7 === undefined || this.form.value.PublicRelations7 === '') ? 0 : this.form.value.PublicRelations7.toString().replace(",", ""));
    this.totalOtherExpen = this.totalOtherExpen + parseFloat((this.form.value.PublicRelations8 === undefined || this.form.value.PublicRelations8 === '') ? 0 : this.form.value.PublicRelations8.toString().replace(",", ""));

    this.totalOperatingExpen = parseFloat((this.form.value.LaborAdvertising1 === undefined || this.form.value.LaborAdvertising1 === '') ? 0 : this.form.value.LaborAdvertising1.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising2 === undefined || this.form.value.LaborAdvertising2 === '') ? 0 : this.form.value.LaborAdvertising2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising3 === undefined || this.form.value.LaborAdvertising3 === '') ? 0 : this.form.value.LaborAdvertising3.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising4 === undefined || this.form.value.LaborAdvertising4 === '') ? 0 : this.form.value.LaborAdvertising4.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising5 === undefined || this.form.value.LaborAdvertising5 === '') ? 0 : this.form.value.LaborAdvertising5.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising6 === undefined || this.form.value.LaborAdvertising6 === '') ? 0 : this.form.value.LaborAdvertising6.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising7 === undefined || this.form.value.LaborAdvertising7 === '') ? 0 : this.form.value.LaborAdvertising7.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborAdvertising8 === undefined || this.form.value.LaborAdvertising8 === '') ? 0 : this.form.value.LaborAdvertising8.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce1 === undefined || this.form.value.LaborSalesForce1 === '') ? 0 : this.form.value.LaborSalesForce1.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce2 === undefined || this.form.value.LaborSalesForce2 === '') ? 0 : this.form.value.LaborSalesForce2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce3 === undefined || this.form.value.LaborSalesForce3 === '') ? 0 : this.form.value.LaborSalesForce3.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce4 === undefined || this.form.value.LaborSalesForce4 === '') ? 0 : this.form.value.LaborSalesForce4.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce5 === undefined || this.form.value.LaborSalesForce5 === '') ? 0 : this.form.value.LaborSalesForce5.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce6 === undefined || this.form.value.LaborSalesForce6 === '') ? 0 : this.form.value.LaborSalesForce6.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce7 === undefined || this.form.value.LaborSalesForce7 === '') ? 0 : this.form.value.LaborSalesForce7.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborSalesForce8 === undefined || this.form.value.LaborSalesForce8 === '') ? 0 : this.form.value.LaborSalesForce8.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions1 === undefined || this.form.value.LaborPromotions1 === '') ? 0 : this.form.value.LaborPromotions1.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions2 === undefined || this.form.value.LaborPromotions2 === '') ? 0 : this.form.value.LaborPromotions2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions2 === undefined || this.form.value.LaborPromotions2 === '') ? 0 : this.form.value.LaborPromotions2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions2 === undefined || this.form.value.LaborPromotions2 === '') ? 0 : this.form.value.LaborPromotions2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions3 === undefined || this.form.value.LaborPromotions3 === '') ? 0 : this.form.value.LaborPromotions3.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions4 === undefined || this.form.value.LaborPromotions4 === '') ? 0 : this.form.value.LaborPromotions4.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions5 === undefined || this.form.value.LaborPromotions5 === '') ? 0 : this.form.value.LaborPromotions5.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions6 === undefined || this.form.value.LaborPromotions6 === '') ? 0 : this.form.value.LaborPromotions6.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions7 === undefined || this.form.value.LaborPromotions7 === '') ? 0 : this.form.value.LaborPromotions7.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPromotions8 === undefined || this.form.value.LaborPromotions8 === '') ? 0 : this.form.value.LaborPromotions8.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations1 === undefined || this.form.value.LaborPublicRelations1 === '') ? 0 : this.form.value.LaborPublicRelations1.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations2 === undefined || this.form.value.LaborPublicRelations2 === '') ? 0 : this.form.value.LaborPublicRelations2.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations3 === undefined || this.form.value.LaborPublicRelations3 === '') ? 0 : this.form.value.LaborPublicRelations3.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations4 === undefined || this.form.value.LaborPublicRelations4 === '') ? 0 : this.form.value.LaborPublicRelations4.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations5 === undefined || this.form.value.LaborPublicRelations5 === '') ? 0 : this.form.value.LaborPublicRelations5.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations6 === undefined || this.form.value.LaborPublicRelations6 === '') ? 0 : this.form.value.LaborPublicRelations6.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations7 === undefined || this.form.value.LaborPublicRelations7 === '') ? 0 : this.form.value.LaborPublicRelations7.toString().replace(",", ""));
    this.totalOperatingExpen = this.totalOperatingExpen + parseFloat((this.form.value.LaborPublicRelations8 === undefined || this.form.value.LaborPublicRelations8 === '') ? 0 : this.form.value.LaborPublicRelations8.toString().replace(",", ""));
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.marketingDecisionList.forEach(element => {
      switch (element.marketingTechniques) {
        case "Advertising":
          switch (element.segment) {
            case "Business":
              element.spending = parseFloat(this.form.value.Advertising1);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising1);
              break;
            case "Small Business":
              element.spending = parseFloat(this.form.value.Advertising2);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising2);
              break;

            case "Corporate contract":
              element.spending = parseFloat(this.form.value.Advertising3);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising3);
              break;

            case "Families":
              element.spending = parseFloat(this.form.value.Advertising4);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising4);
              break;

            case "Afluent Mature Travelers":
              element.spending = parseFloat(this.form.value.Advertising5);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising5);
              break;

            case "International leisure travelers":
              element.spending = parseFloat(this.form.value.Advertising6);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising6);
              break;

            case "Corporate/Business Meetings":
              element.spending = parseFloat(this.form.value.Advertising7);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising7);
              break;

            case "Association Meetings":
              element.spending = parseFloat(this.form.value.Advertising8);
              element.laborSpending = parseFloat(this.form.value.LaborAdvertising8);
              break;
            default: break;
          }
          break;
        case "Sales Force":
          switch (element.segment) {
            case "Business":
              element.spending = parseFloat(this.form.value.SalesForce1);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce1);
              break;
            case "Small Business":
              element.spending = parseFloat(this.form.value.SalesForce2);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce2);
              break;

            case "Corporate contract":
              element.spending = parseFloat(this.form.value.SalesForce3);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce3);
              break;

            case "Families":
              element.spending = parseFloat(this.form.value.SalesForce4);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce4);
              break;

            case "Afluent Mature Travelers":
              element.spending = parseFloat(this.form.value.SalesForce5);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce5);
              break;

            case "International leisure travelers":
              element.spending = parseFloat(this.form.value.SalesForce6);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce6);
              break;

            case "Corporate/Business Meetings":
              element.spending = parseFloat(this.form.value.SalesForce7);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce7);
              break;

            case "Association Meetings":
              element.spending = parseFloat(this.form.value.SalesForce8);
              element.laborSpending = parseFloat(this.form.value.LaborSalesForce8);
              break;
            default: break;
          }
          break;
        case "Promotions":
          switch (element.segment) {
            case "Business":
              element.spending = parseFloat(this.form.value.Promotions1);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions1);
              break;

            case "Small Business":
              element.spending = parseFloat(this.form.value.Promotions2);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions2);
              break;

            case "Corporate contract":
              element.spending = parseFloat(this.form.value.Promotions3);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions3);
              break;

            case "Families":
              element.spending = parseFloat(this.form.value.Promotions4);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions4);
              break;

            case "Afluent Mature Travelers":
              element.spending = parseFloat(this.form.value.Promotions5);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions5);
              break;

            case "International leisure travelers":
              element.spending = parseFloat(this.form.value.Promotions6);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions6);
              break;

            case "Corporate/Business Meetings":
              element.spending = parseFloat(this.form.value.Promotions7);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions7);
              break;

            case "Association Meetings":
              element.spending = parseFloat(this.form.value.Promotions8);
              element.laborSpending = parseFloat(this.form.value.LaborPromotions8);
              break;
            default: break;
          }
          break;
        case "Public Relations":
          switch (element.segment) {
            case "Business":
              element.spending = parseFloat(this.form.value.PublicRelations1);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations1);
              break;

            case "Small Business":
              element.spending = parseFloat(this.form.value.PublicRelations2);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations2);
              break;

            case "Corporate contract":
              element.spending = parseFloat(this.form.value.PublicRelations3);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations3);
              break;

            case "Families":
              element.spending = parseFloat(this.form.value.PublicRelations4);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations4);
              break;

            case "Afluent Mature Travelers":
              element.spending = parseFloat(this.form.value.PublicRelations5);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations5);
              break;

            case "International leisure travelers":
              element.spending = parseFloat(this.form.value.PublicRelations6);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations6);
              break;

            case "Corporate/Business Meetings":
              element.spending = parseFloat(this.form.value.PublicRelations7);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations7);
              break;

            case "Association Meetings":
              element.spending = parseFloat(this.form.value.PublicRelations8);
              element.laborSpending = parseFloat(this.form.value.LaborPublicRelations8);
              break;
            default: break;
          }
          break;
        default: break;
      }
    });
    this.studentService.UpdateMarketingDetails(this.marketingDecisionList).subscribe((x) => {
      this.marketDecisionList();
    });
  }

  private createForm(): FormGroup {
    return this.fb.group({
      Advertising1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Advertising8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      SalesForce8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Promotions8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      PublicRelations8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborAdvertising8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborSalesForce8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPromotions8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      LaborPublicRelations8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    });
  }

}
