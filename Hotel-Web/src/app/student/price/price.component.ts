import { Component } from '@angular/core';
import { StudentService } from '../student.service';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PriceDecision } from 'src/app/shared/class/model/classSession.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-price',
  templateUrl: './price.component.html',
  styleUrls: ['./price.component.css']
})
export class PriceComponent {
  form: FormGroup;
  priceDecision: PriceDecision[] = [];
  submitted = false;
  errorMsg: string = "";

  constructor(
    private studentService: StudentService, private fb: FormBuilder,private _snackBar: MatSnackBar) {
    this.form = this.createForm();
  }

  ngOnInit(): void {
    this.priceDecisionList();
  }

  private priceDecisionList() {
    this.studentService.PriceDecisionList().subscribe((data) => {
      this.priceDecision = data;      
      this.priceDecision.forEach(element => {
        switch (element.distributionChannel) {
          case "Direct": {
            if (element.weekday === true) {
              switch (element.segment) {
                case "Business":
                  this.form.patchValue({ DweekDay1: element.priceNOFormat });
                  break;
                case "Small Business":
                  this.form.patchValue({ DweekDay2: element.priceNOFormat });
                  break;
                case "Corporate contract":
                  this.form.patchValue({ DweekDay3: element.priceNOFormat });

                  break;

                case "Families":
                  this.form.patchValue({ DweekDay4: element.priceNOFormat });

                  break;

                case "Afluent Mature Travelers":
                  this.form.patchValue({ DweekDay5: element.priceNOFormat });

                  break;

                case "International leisure travelers":
                  this.form.patchValue({ DweekDay6: element.priceNOFormat });
                  break;

                case "Corporate/Business Meetings":
                  this.form.patchValue({ DweekDay7: element.priceNOFormat });
                  break;

                case "Association Meetings":
                  this.form.patchValue({ DweekDay8: element.priceNOFormat });
                  break;
                default: break;

              }
            }
            if (element.weekday === false) {
              switch (element.segment) {
                case "Business":
                  this.form.patchValue({ Dweekend1: element.priceNOFormat });
                  break;
                case "Small Business":
                  this.form.patchValue({ Dweekend2: element.priceNOFormat });
                  break;
                case "Corporate contract":
                  this.form.patchValue({ Dweekend3: element.priceNOFormat });
                  break;
                case "Families":
                  this.form.patchValue({ Dweekend4: element.priceNOFormat });
                  break;
                case "Afluent Mature Travelers":
                  this.form.patchValue({ Dweekend5: element.priceNOFormat });

                  break;

                case "International leisure travelers":
                  this.form.patchValue({ Dweekend6: element.priceNOFormat });
                  break;

                case "Corporate/Business Meetings":
                  this.form.patchValue({ Dweekend7: element.priceNOFormat });
                  break;

                case "Association Meetings":
                  this.form.patchValue({ Dweekend8: element.priceNOFormat });
                  break;
                default: break;

              }
            }
          }
            break;
          case "Online Travel Agent":
            {
              if (element.weekday == true) {
                switch (element.segment) {
                  case "Business":
                    this.form.patchValue({ OnweekDay1: element.priceNOFormat });
                    break;
                  case "Small Business":
                    this.form.patchValue({ OnweekDay2: element.priceNOFormat });
                    break;
                  case "Corporate contract":
                    this.form.patchValue({ OnweekDay3: element.priceNOFormat });
                    break;
                  case "Families":
                    this.form.patchValue({ OnweekDay4: element.priceNOFormat });
                    break;
                  case "Afluent Mature Travelers":
                    this.form.patchValue({ OnweekDay5: element.priceNOFormat });
                    break;
                  case "International leisure travelers":
                    this.form.patchValue({ OnweekDay6: element.priceNOFormat });
                    break;
                  case "Corporate/Business Meetings":
                    this.form.patchValue({ OnweekDay7: element.priceNOFormat });
                    break;
                  case "Association Meetings":
                    this.form.patchValue({ OnweekDay8: element.priceNOFormat });
                    break;
                  default: break;
                }
              }
              if (element.weekday == false) {
                switch (element.segment) {
                  case "Business":
                    this.form.patchValue({ Onweekend1: element.priceNOFormat });
                    break;
                  case "Small Business":
                    this.form.patchValue({ Onweekend2: element.priceNOFormat });
                    break;
                  case "Corporate contract":
                    this.form.patchValue({ Onweekend3: element.priceNOFormat });
                    break;
                  case "Families":
                    this.form.patchValue({ Onweekend4: element.priceNOFormat });
                    break;
                  case "Afluent Mature Travelers":
                    this.form.patchValue({ Onweekend5: element.priceNOFormat });
                    break;
                  case "International leisure travelers":
                    this.form.patchValue({ Onweekend6: element.priceNOFormat });
                    break;
                  case "Corporate/Business Meetings":
                    this.form.patchValue({ Onweekend7: element.priceNOFormat });
                    break;
                  case "Association Meetings":
                    this.form.patchValue({ Onweekend8: element.priceNOFormat });
                    break;
                  default: break;
                }
              }
            }
            break;
            case "Travel Agent":
              {
                  if (element.weekday == true)
                  {
                      switch (element.segment)
                      {
                          case "Business":
                            this.form.patchValue({ WweekDay1: element.priceNOFormat });
                              break;

                          case "Small Business":
                            this.form.patchValue({ WweekDay2: element.priceNOFormat });
                              break;

                          case "Corporate contract":
                            this.form.patchValue({ WweekDay3: element.priceNOFormat });
                              break;

                          case "Families":
                            this.form.patchValue({ WweekDay4: element.priceNOFormat });
                              break;

                          case "Afluent Mature Travelers":
                            this.form.patchValue({ WweekDay5: element.priceNOFormat });
                              break;

                          case "International leisure travelers":
                            this.form.patchValue({ WweekDay6: element.priceNOFormat });
                              break;

                          case "Corporate/Business Meetings":
                            this.form.patchValue({ WweekDay7: element.priceNOFormat });
                              break;

                          case "Association Meetings":
                            this.form.patchValue({ WweekDay8: element.priceNOFormat });
                              break;
                          default: break;
                      }
                  }
                  if (element.weekday == false)
                  {
                      switch (element.segment)
                      {
                          case "Business":
                            this.form.patchValue({ Wweekend1: element.priceNOFormat });
                              break;

                          case "Small Business":
                            this.form.patchValue({ Wweekend2: element.priceNOFormat });
                              break;

                          case "Corporate contract":
                            this.form.patchValue({ Wweekend3: element.priceNOFormat });
                              break;

                          case "Families":
                            this.form.patchValue({ Wweekend4: element.priceNOFormat });
                              break;

                          case "Afluent Mature Travelers":
                            this.form.patchValue({ Wweekend5: element.priceNOFormat });
                              break;

                          case "International leisure travelers":
                            this.form.patchValue({ Wweekend6: element.priceNOFormat });
                              break;

                          case "Corporate/Business Meetings":
                            this.form.patchValue({ Wweekend7: element.priceNOFormat });
                              break;

                          case "Association Meetings":
                            this.form.patchValue({ Wweekend8: element.priceNOFormat });
                              break;
                          default: break;
                      }
                  }
              }
              break;
              case "Opaque":
                {
                    if (element.weekday == true)
                    {
                        switch (element.segment)
                        {
                            case "Business":
                              this.form.patchValue({ OpweekDay1: element.priceNOFormat });
                              break;
                               
                            case "Small Business":
                              this.form.patchValue({ OpweekDay2: element.priceNOFormat });
                                break;

                            case "Corporate contract":
                              this.form.patchValue({ OpweekDay3: element.priceNOFormat });
                                break;

                            case "Families":
                              this.form.patchValue({ OpweekDay4: element.priceNOFormat });
                                break;

                            case "Afluent Mature Travelers":
                              this.form.patchValue({ OpweekDay5: element.priceNOFormat });
                                break;

                            case "International leisure travelers":
                              this.form.patchValue({ OpweekDay6: element.priceNOFormat });
                                break;

                            case "Corporate/Business Meetings":
                              this.form.patchValue({ OpweekDay7: element.priceNOFormat });
                                break;

                            case "Association Meetings":
                              this.form.patchValue({ OpweekDay8: element.priceNOFormat });
                                break;
                            default: break;
                        }
                    }
                    if (element.weekday == false)
                    {
                        switch (element.segment)
                        {
                            case "Business":
                              this.form.patchValue({ Opweekend1: element.priceNOFormat });
                                break;

                            case "Small Business":
                              this.form.patchValue({ Opweekend2: element.priceNOFormat });
                                break;

                            case "Corporate contract":
                              this.form.patchValue({ Opweekend3: element.priceNOFormat });
                                break;

                            case "Families":
                              this.form.patchValue({ Opweekend4: element.priceNOFormat });
                                break;

                            case "Afluent Mature Travelers":
                              this.form.patchValue({ Opweekend5: element.priceNOFormat });
                                break;

                            case "International leisure travelers":
                              this.form.patchValue({ Opweekend6: element.priceNOFormat });
                                break;

                            case "Corporate/Business Meetings":
                              this.form.patchValue({ Opweekend7: element.priceNOFormat });
                                break;

                            case "Association Meetings":
                              this.form.patchValue({ Opweekend8: element.priceNOFormat });
                                break;
                            default: break;
                        }
                    }
                }
                break;
            default: break;  
        }
      });
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.priceDecision.forEach(element => {
      switch (element.distributionChannel) {
        case "Direct": {
          if (element.weekday === true) {
            switch (element.segment) {
              case "Business":
                element.price = parseFloat(this.form.value.DweekDay1);
                
                break;
              case "Small Business":
                element.price = parseFloat(this.form.value.DweekDay2);
            
                break;
              case "Corporate contract":
                element.price = parseFloat(this.form.value.DweekDay3);

                break;

              case "Families":
                element.price = parseFloat(this.form.value.DweekDay4);

                break;

              case "Afluent Mature Travelers":
                element.price = parseFloat(this.form.value.DweekDay5);

                break;

              case "International leisure travelers":
                element.price = parseFloat(this.form.value.DweekDay6);
                break;

              case "Corporate/Business Meetings":
                element.price = parseFloat(this.form.value.DweekDay7);
                break;

              case "Association Meetings":
                element.price = parseFloat(this.form.value.DweekDay8);
                break;
              default: break;

            }
          }
          if (element.weekday === false) {
            switch (element.segment) {
              case "Business":
                element.price = parseFloat(this.form.value.Dweekend1);
                break;
              case "Small Business":
                element.price = parseFloat(this.form.value.Dweekend2);
                break;
              case "Corporate contract":
                element.price = parseFloat(this.form.value.Dweekend3);
                break;
              case "Families":
                element.price = parseFloat(this.form.value.Dweekend4);
                break;
              case "Afluent Mature Travelers":
                element.price = parseFloat(this.form.value.Dweekend5);
                break;
              case "International leisure travelers":
                element.price = parseFloat(this.form.value.Dweekend6);
                break;
              case "Corporate/Business Meetings":
                element.price = parseFloat(this.form.value.Dweekend7);
                break;
              case "Association Meetings":
                element.price = parseFloat(this.form.value.Dweekend8);
                break;
              default: break;
            }
          }
        }
          break;
        case "Online Travel Agent":
          {
            if (element.weekday == true) {
              switch (element.segment) {
                case "Business":
                  element.price = parseFloat(this.form.value.OnweekDay1);
                  break;
                case "Small Business":
                  element.price = parseFloat(this.form.value.OnweekDay2);
                  break;
                case "Corporate contract":
                  element.price = parseFloat(this.form.value.OnweekDay3);
                  break;
                case "Families":
                  element.price = parseFloat(this.form.value.OnweekDay4);
                  break;
                case "Afluent Mature Travelers":
                  element.price = parseFloat(this.form.value.OnweekDay5);
                  break;
                case "International leisure travelers":
                  element.price = parseFloat(this.form.value.OnweekDay6);
                  break;
                case "Corporate/Business Meetings":
                  element.price = parseFloat(this.form.value.OnweekDay7);
                  break;
                case "Association Meetings":
                  element.price = parseFloat(this.form.value.OnweekDay8);
                  break;
                default: break;
              }
            }
            if (element.weekday == false) {
              switch (element.segment) {
                case "Business":
                  element.price = parseFloat(this.form.value.Onweekend1);
                  break;
                case "Small Business":
                  element.price = parseFloat(this.form.value.Onweekend2);
                  break;
                case "Corporate contract":
                  element.price = parseFloat(this.form.value.Onweekend3);
                  break;
                case "Families":
                  element.price = parseFloat(this.form.value.Onweekend4);
                  break;
                case "Afluent Mature Travelers":
                  element.price = parseFloat(this.form.value.Onweekend5);
                  break;
                case "International leisure travelers":
                  element.price = parseFloat(this.form.value.Onweekend6);
                  break;
                case "Corporate/Business Meetings":
                  element.price = parseFloat(this.form.value.Onweekend7);
                  break;
                case "Association Meetings":
                  element.price = parseFloat(this.form.value.Onweekend8);
                  break;
                default: break;
              }
            }
          }
          break;
          case "Travel Agent":
            {
                if (element.weekday == true)
                {
                    switch (element.segment)
                    {
                        case "Business":
                          element.price = parseFloat(this.form.value.WweekDay1);
                            break;

                        case "Small Business":
                          element.price = parseFloat(this.form.value.WweekDay2);
                            break;

                        case "Corporate contract":
                          element.price = parseFloat(this.form.value.WweekDay3);
                            break;

                        case "Families":
                          element.price = parseFloat(this.form.value.WweekDay4);
                            break;

                        case "Afluent Mature Travelers":
                          element.price = parseFloat(this.form.value.WweekDay5);
                            break;

                        case "International leisure travelers":
                          element.price = parseFloat(this.form.value.WweekDay6);
                            break;

                        case "Corporate/Business Meetings":
                          element.price = parseFloat(this.form.value.WweekDay7);
                            break;

                        case "Association Meetings":
                          element.price = parseFloat(this.form.value.WweekDay8);
                            break;
                        default: break;
                    }
                }
                if (element.weekday == false)
                {
                    switch (element.segment)
                    {
                        case "Business":
                          element.price = parseFloat(this.form.value.Wweekend1);
                            break;

                        case "Small Business":
                          element.price = parseFloat(this.form.value.Wweekend2);
                            break;

                        case "Corporate contract":
                          element.price = parseFloat(this.form.value.Wweekend3);
                            break;

                        case "Families":
                          element.price = parseFloat(this.form.value.Wweekend4);
                            break;

                        case "Afluent Mature Travelers":
                          element.price = parseFloat(this.form.value.Wweekend5);
                            break;

                        case "International leisure travelers":
                          element.price = parseFloat(this.form.value.Wweekend6);
                            break;

                        case "Corporate/Business Meetings":
                          element.price = parseFloat(this.form.value.Wweekend7);
                            break;

                        case "Association Meetings":
                          element.price = parseFloat(this.form.value.Wweekend8);
                            break;
                        default: break;
                    }
                }
            }
            break;
            case "Opaque":
              {
                  if (element.weekday == true)
                  {
                      switch (element.segment)
                      {
                          case "Business":
                            element.price = parseFloat(this.form.value.OpweekDay1);
                            break;
                             
                          case "Small Business":
                            element.price = parseFloat(this.form.value.OpweekDay2);
                              break;

                          case "Corporate contract":
                            element.price = parseFloat(this.form.value.OpweekDay3);
                              break;

                          case "Families":
                            element.price = parseFloat(this.form.value.OpweekDay4);
                              break;

                          case "Afluent Mature Travelers":
                            element.price = parseFloat(this.form.value.OpweekDay5);
                              break;

                          case "International leisure travelers":
                            element.price = parseFloat(this.form.value.OpweekDay6);
                              break;

                          case "Corporate/Business Meetings":
                            element.price = parseFloat(this.form.value.OpweekDay7);
                              break;

                          case "Association Meetings":
                            element.price = parseFloat(this.form.value.OpweekDay8);
                              break;
                          default: break;
                      }
                  }
                  if (element.weekday == false)
                  {
                      switch (element.segment)
                      {
                          case "Business":
                            element.price = parseFloat(this.form.value.Opweekend1);
                              break;

                          case "Small Business":
                            element.price = parseFloat(this.form.value.Opweekend2);
                              break;

                          case "Corporate contract":
                            element.price = parseFloat(this.form.value.Opweekend3);
                              break;

                          case "Families":
                            element.price = parseFloat(this.form.value.Opweekend4);
                              break;

                          case "Afluent Mature Travelers":
                            element.price = parseFloat(this.form.value.Opweekend5);
                              break;

                          case "International leisure travelers":
                            element.price = parseFloat(this.form.value.Opweekend6);
                              break;

                          case "Corporate/Business Meetings":
                            element.price = parseFloat(this.form.value.Opweekend7);
                              break;

                          case "Association Meetings":
                            element.price = parseFloat(this.form.value.Opweekend8);
                              break;
                          default: break;
                      }
                  }
              }
              break;
          default: break;  
      }
    });

    this.studentService.PriceDecisionUpdate(this.priceDecision).subscribe((x) => {
      this._snackBar.open('Price successfully updated');
    });
}

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private createForm(): FormGroup {
    return this.fb.group({
      DweekDay1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      DweekDay8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Dweekend8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      WweekDay8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Wweekend8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OnweekDay8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Onweekend8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      OpweekDay8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend1: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend3: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend4: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend5: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend2: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend6: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend7: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
      Opweekend8: ['', [Validators.required, Validators.pattern("^[0-9]*$")]]
    });
  }
}
