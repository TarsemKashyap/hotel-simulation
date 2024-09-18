import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { StudentService } from '../student.service';
import {
  AttributeDecision,
  DecimalValidator,
} from 'src/app/shared/class/model/classSession.model';
import { SessionStore } from 'src/app/store';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { StudentRoles } from 'src/app/shared/class/model/StudentRoles';
import { Utility } from 'src/app/shared/utility';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.css'],
})
export class AttributeComponent {
  form: FormGroup;
  submitted = false;
  totalAccumulated: string;
  totalAmenities: string;
  totalOther: string;
  totalLabour: string;
  totalExpensesAllocated: string;
  attributeDecisions: AttributeDecision[] = [];
  selectedRoles: any = [];
  selectedRoleList: any = [];
  currentRole: number[] = [];

  ngOnInit(): void {
    this.attributeDecisionList();
    this.form = this.createForm();
    this.form.valueChanges.subscribe((p) => {
      const keyVal = Object.entries<number>(p);
      const map: { [key: string]: number } = {};

      for (const [name, value] of keyVal) {
        const key = name.replace(/\d+/g, '');
        let val = map[key];
        let newval = val ? val + value : value;
        map[key] = newval;
      }

      let newtotal = Object.values(map).reduce((p, c) => p + c, 0);

      this.totalOther = map['Other'].toString();
      this.totalLabour = map['Labour'].toString();
      this.totalAmenities = map['Amenities'].toString();
      this.totalExpensesAllocated = newtotal.toString();
    });
  }

  constructor(
    private studentService: StudentService,
    private fb: FormBuilder,
    private sessionStore: SessionStore,
    private router: Router,
    private _snackBar: MatSnackBar
  ) {
    this.currentRole = this.sessionStore.GetRoleids();
    if (!this.currentRole) {
      this.router.navigate(['']);
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  private attributeDecisionList() {
    this.studentService.AttributeDecisionList().subscribe((data) => {
      this.totalAccumulated = data
        .filter((p) => (p.accumulatedCapital ? true : false))
        .map((p) => p.accumulatedCapital)
        .reduce((p, c) => p + c, 0)
        .toString();

      this.patchForm(data);

      this.disableFieldFB();
      this.disableFormRevenueManager();
      this.disableFieldRoomManager();
    });
  }

  private patchForm(data: AttributeDecision[]) {
    const formData: { [key: string]: number } = {};
    data.forEach((ele, i) => {
      formData[`Accumulated${i + 1}`] = ele.accumulatedCapital;
      formData[`Amenities${i + 1}`] = ele.newCapital;
      formData[`Other${i + 1}`] = ele.operationBudget;
      formData[`Labour${i + 1}`] = ele.laborBudget;
    });
    this.form.patchValue(formData);
    this.attributeDecisions = data;
    console.log({ totalAccumu: this.totalAccumulated });
  }

  private disableFormRevenueManager() {
    if (this.currentRole.includes(StudentRoles.RoomManager)) {
      this.form.controls['Amenities6'].disable();
      this.form.controls['Other6'].disable();
      this.form.controls['Labour6'].disable();
      this.form.controls['Amenities7'].disable();
      this.form.controls['Other7'].disable();
      this.form.controls['Labour7'].disable();
      this.form.controls['Amenities8'].disable();
      this.form.controls['Other8'].disable();
      this.form.controls['Labour8'].disable();
      this.form.controls['Amenities9'].disable();
      this.form.controls['Other9'].disable();
      this.form.controls['Labour9'].disable();
      this.form.controls['Amenities10'].disable();
      this.form.controls['Other10'].disable();
      this.form.controls['Labour10'].disable();
      this.form.controls['Amenities11'].disable();
      this.form.controls['Other11'].disable();
      this.form.controls['Labour11'].disable();
      this.form.controls['Amenities12'].disable();
      this.form.controls['Other12'].disable();
      this.form.controls['Labour12'].disable();
      this.form.controls['Amenities13'].disable();
      this.form.controls['Other13'].disable();
      this.form.controls['Labour13'].disable();
      this.form.controls['Amenities14'].disable();
      this.form.controls['Other14'].disable();
      this.form.controls['Labour14'].disable();
      this.form.controls['Amenities15'].disable();
      this.form.controls['Other15'].disable();
      this.form.controls['Labour15'].disable();
      this.form.controls['Amenities16'].disable();
      this.form.controls['Other16'].disable();
      this.form.controls['Labour16'].disable();
      this.form.controls['Amenities17'].disable();
      this.form.controls['Other17'].disable();
      this.form.controls['Labour17'].disable();
      this.form.controls['Amenities18'].disable();
      this.form.controls['Other18'].disable();
      this.form.controls['Labour18'].disable();
      this.form.controls['Amenities19'].disable();
      this.form.controls['Other19'].disable();
      this.form.controls['Labour19'].disable();
      this.form.controls['Amenities20'].disable();
      this.form.controls['Other20'].disable();
      this.form.controls['Labour20'].disable();
    }
  }

  private disableFieldFB() {
    if (this.currentRole.includes(StudentRoles.FBManager)) {
      this.form.controls['Amenities1'].disable();
      this.form.controls['Other1'].disable();
      this.form.controls['Labour1'].disable();
      this.form.controls['Amenities2'].disable();
      this.form.controls['Other2'].disable();
      this.form.controls['Labour2'].disable();
      this.form.controls['Amenities3'].disable();
      this.form.controls['Other3'].disable();
      this.form.controls['Labour3'].disable();
      this.form.controls['Amenities4'].disable();
      this.form.controls['Other4'].disable();
      this.form.controls['Labour4'].disable();
      this.form.controls['Amenities5'].disable();
      this.form.controls['Other5'].disable();
      this.form.controls['Labour5'].disable();
      this.form.controls['Amenities14'].disable();
      this.form.controls['Other14'].disable();
      this.form.controls['Labour14'].disable();
      this.form.controls['Amenities15'].disable();
      this.form.controls['Other15'].disable();
      this.form.controls['Labour15'].disable();
      this.form.controls['Amenities16'].disable();
      this.form.controls['Other16'].disable();
      this.form.controls['Labour16'].disable();
      this.form.controls['Amenities17'].disable();
      this.form.controls['Other17'].disable();
      this.form.controls['Labour17'].disable();
      this.form.controls['Amenities18'].disable();
      this.form.controls['Other18'].disable();
      this.form.controls['Labour18'].disable();
      this.form.controls['Amenities19'].disable();
      this.form.controls['Other19'].disable();
      this.form.controls['Labour19'].disable();
      this.form.controls['Amenities20'].disable();
      this.form.controls['Other20'].disable();
      this.form.controls['Labour20'].disable();
    }
  }

  private disableFieldRoomManager() {
    if (this.currentRole.includes(StudentRoles.RevenueManager)) {
      this.form.controls['Amenities1'].disable();
      this.form.controls['Other1'].disable();
      this.form.controls['Labour1'].disable();
      this.form.controls['Amenities2'].disable();
      this.form.controls['Other2'].disable();
      this.form.controls['Labour2'].disable();
      this.form.controls['Amenities3'].disable();
      this.form.controls['Other3'].disable();
      this.form.controls['Labour3'].disable();
      this.form.controls['Amenities4'].disable();
      this.form.controls['Other4'].disable();
      this.form.controls['Labour4'].disable();
      this.form.controls['Amenities5'].disable();
      this.form.controls['Other5'].disable();
      this.form.controls['Labour5'].disable();
      this.form.controls['Amenities7'].disable();
      this.form.controls['Other7'].disable();
      this.form.controls['Labour7'].disable();
      this.form.controls['Amenities8'].disable();
      this.form.controls['Other8'].disable();
      this.form.controls['Labour8'].disable();
      this.form.controls['Amenities9'].disable();
      this.form.controls['Other9'].disable();
      this.form.controls['Labour9'].disable();
      this.form.controls['Amenities10'].disable();
      this.form.controls['Other10'].disable();
      this.form.controls['Labour10'].disable();
      this.form.controls['Amenities11'].disable();
      this.form.controls['Other11'].disable();
      this.form.controls['Labour11'].disable();
      this.form.controls['Amenities12'].disable();
      this.form.controls['Other12'].disable();
      this.form.controls['Labour12'].disable();
      this.form.controls['Amenities13'].disable();
      this.form.controls['Other13'].disable();
      this.form.controls['Labour13'].disable();
    }
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.attributeDecisions.forEach((element) => {
      switch (element.attribute) {
        case 'Spa': {
          element.newCapital = this.form.value.Amenities1;
          element.operationBudget = this.form.value.Other1;
          element.laborBudget = this.form.value.Labour1;
          break;
        }
        case 'Fitness Center': {
          element.newCapital = this.form.value.Amenities2;
          element.operationBudget = this.form.value.Other2;
          element.laborBudget = this.form.value.Labour2;
          break;
        }
        case 'Business Center': {
          element.newCapital = this.form.value.Amenities3;
          element.operationBudget = this.form.value.Other3;
          element.laborBudget = this.form.value.Labour3;
          break;
        }
        case 'Golf Course': {
          element.newCapital = this.form.value.Amenities4;
          element.operationBudget = this.form.value.Other4;
          element.laborBudget = this.form.value.Labour4;
          break;
        }
        case 'Other Recreation Facilities - Pools, game rooms, tennis courts, ect': {
          element.newCapital = this.form.value.Amenities5;
          element.operationBudget = this.form.value.Other5;
          element.laborBudget = this.form.value.Labour5;
          break;
        }
        case 'Management/Sales Attention': {
          element.newCapital = this.form.value.Amenities6;
          element.operationBudget = this.form.value.Other6;
          element.laborBudget = this.form.value.Labour6;
          break;
        }
        case 'Resturants': {
          element.newCapital = this.form.value.Amenities7;
          element.operationBudget = this.form.value.Other7;
          element.laborBudget = this.form.value.Labour7;
          break;
        }
        case 'Bars': {
          element.newCapital = this.form.value.Amenities8;
          element.operationBudget = this.form.value.Other8;
          element.laborBudget = this.form.value.Labour8;
          break;
        }
        case 'Room Service': {
          element.newCapital = this.form.value.Amenities9;
          element.operationBudget = this.form.value.Other9;
          element.laborBudget = this.form.value.Labour9;
          break;
        }
        case 'Banquet & Catering': {
          element.newCapital = this.form.value.Amenities10;
          element.operationBudget = this.form.value.Other10;
          element.laborBudget = this.form.value.Labour10;
          break;
        }
        case 'Meeting Rooms': {
          element.newCapital = this.form.value.Amenities11;
          element.operationBudget = this.form.value.Other11;
          element.laborBudget = this.form.value.Labour11;
          break;
        }
        case 'Entertainment': {
          element.newCapital = this.form.value.Amenities12;
          element.operationBudget = this.form.value.Other12;
          element.laborBudget = this.form.value.Labour12;
          break;
        }
        case 'Courtesy(FB)': {
          element.newCapital = this.form.value.Amenities13;
          element.operationBudget = this.form.value.Other13;
          element.laborBudget = this.form.value.Labour13;
          break;
        }
        case 'Guest Rooms': {
          element.newCapital = this.form.value.Amenities14;
          element.operationBudget = this.form.value.Other14;
          element.laborBudget = this.form.value.Labour14;
          break;
        }
        case 'Reservations': {
          element.newCapital = this.form.value.Amenities15;
          element.operationBudget = this.form.value.Other15;
          element.laborBudget = this.form.value.Labour15;
          break;
        }

        case 'Guest Check in/Guest Check out': {
          element.newCapital = this.form.value.Amenities16;
          element.operationBudget = this.form.value.Other16;
          element.laborBudget = this.form.value.Labour16;
          break;
        }
        case 'Concierge': {
          element.newCapital = this.form.value.Amenities17;
          element.operationBudget = this.form.value.Other17;
          element.laborBudget = this.form.value.Labour17;
          break;
        }
        case 'Housekeeping': {
          element.newCapital = this.form.value.Amenities18;
          element.operationBudget = this.form.value.Other18;
          element.laborBudget = this.form.value.Labour18;
          break;
        }
        case 'Maintanence and security': {
          element.newCapital = this.form.value.Amenities19;
          element.operationBudget = this.form.value.Other19;
          element.laborBudget = this.form.value.Labour19;
          break;
        }
        case 'Courtesy (Rooms)': {
          element.newCapital = this.form.value.Amenities20;
          element.operationBudget = this.form.value.Other20;
          element.laborBudget = this.form.value.Labour20;
          break;
        }
      }
    });

    // const postData = this.attributeDecisions.map((d) => {
    //   let copy = Object.assign({}, d);
    //   copy.accumulatedCapital = Utility.formatNumber(d.accumulatedCapital.toString());
    //   copy.newCapital = Utility.formatNumber(d.newCapital.toString());
    //   copy.operationBudget = Utility.formatNumber(d.operationBudget.toString());
    //   copy.laborBudget = Utility.formatNumber(d.laborBudget.toString());
    //   return copy;
    // });

    this.studentService
      .AttributeDecisionUpdate(this.attributeDecisions)
      .subscribe((x) => {
        this._snackBar.open('Attribute Updated successfully', 'Close', {
          duration: 3000,
        });
        this.attributeDecisionList();
      });
  }

  private createForm(): FormGroup {
    return this.fb.group({
      Accumulated1: [{ value: '', disabled: true }],
      Accumulated2: [{ value: '', disabled: true }],
      Accumulated3: [{ value: '', disabled: true }],
      Accumulated4: [{ value: '', disabled: true }],
      Accumulated5: [{ value: '', disabled: true }],
      Accumulated6: [{ value: '', disabled: true }],
      Accumulated7: [{ value: '', disabled: true }],
      Accumulated8: [{ value: '', disabled: true }],
      Accumulated9: [{ value: '', disabled: true }],
      Accumulated10: [{ value: '', disabled: true }],
      Accumulated11: [{ value: '', disabled: true }],
      Accumulated12: [{ value: '', disabled: true }],
      Accumulated13: [{ value: '', disabled: true }],
      Accumulated14: [{ value: '', disabled: true }],
      Accumulated15: [{ value: '', disabled: true }],
      Accumulated16: [{ value: '', disabled: true }],
      Accumulated17: [{ value: '', disabled: true }],
      Accumulated18: [{ value: '', disabled: true }],
      Accumulated19: [{ value: '', disabled: true }],
      Accumulated20: [{ value: '', disabled: true }],
      Amenities1: ['', [Validators.required, DecimalValidator]],
      Amenities2: ['', [Validators.required, DecimalValidator]],
      Amenities3: ['', [Validators.required, DecimalValidator]],
      Amenities4: ['', [Validators.required, DecimalValidator]],
      Amenities5: ['', [Validators.required, DecimalValidator]],
      Amenities6: ['', [Validators.required, DecimalValidator]],
      Amenities7: ['', [Validators.required, DecimalValidator]],
      Amenities8: ['', [Validators.required, DecimalValidator]],
      Amenities9: ['', [Validators.required, DecimalValidator]],
      Amenities10: ['', [Validators.required, DecimalValidator]],
      Amenities11: ['', [Validators.required, DecimalValidator]],
      Amenities12: ['', [Validators.required, DecimalValidator]],
      Amenities13: ['', [Validators.required, DecimalValidator]],
      Amenities14: ['', [Validators.required, DecimalValidator]],
      Amenities15: ['', [Validators.required, DecimalValidator]],
      Amenities16: ['', [Validators.required, DecimalValidator]],
      Amenities17: ['', [Validators.required, DecimalValidator]],
      Amenities18: ['', [Validators.required, DecimalValidator]],
      Amenities19: ['', [Validators.required, DecimalValidator]],
      Amenities20: ['', [Validators.required, DecimalValidator]],
      Other1: ['', [Validators.required, DecimalValidator]],
      Other2: ['', [Validators.required, DecimalValidator]],
      Other3: ['', [Validators.required, DecimalValidator]],
      Other4: ['', [Validators.required, DecimalValidator]],
      Other5: ['', [Validators.required, DecimalValidator]],
      Other6: ['', [Validators.required, DecimalValidator]],
      Other7: ['', [Validators.required, DecimalValidator]],
      Other8: ['', [Validators.required, DecimalValidator]],
      Other9: ['', [Validators.required, DecimalValidator]],
      Other10: ['', [Validators.required, DecimalValidator]],
      Other11: ['', [Validators.required, DecimalValidator]],
      Other12: ['', [Validators.required, DecimalValidator]],
      Other13: ['', [Validators.required, DecimalValidator]],
      Other14: ['', [Validators.required, DecimalValidator]],
      Other15: ['', [Validators.required, DecimalValidator]],
      Other16: ['', [Validators.required, DecimalValidator]],
      Other17: ['', [Validators.required, DecimalValidator]],
      Other18: ['', [Validators.required, DecimalValidator]],
      Other19: ['', [Validators.required, DecimalValidator]],
      Other20: ['', [Validators.required, DecimalValidator]],
      Labour1: ['', [Validators.required, DecimalValidator]],
      Labour2: ['', [Validators.required, DecimalValidator]],
      Labour3: ['', [Validators.required, DecimalValidator]],
      Labour4: ['', [Validators.required, DecimalValidator]],
      Labour5: ['', [Validators.required, DecimalValidator]],
      Labour6: ['', [Validators.required, DecimalValidator]],
      Labour7: ['', [Validators.required, DecimalValidator]],
      Labour8: ['', [Validators.required, DecimalValidator]],
      Labour9: ['', [Validators.required, DecimalValidator]],
      Labour10: ['', [Validators.required, DecimalValidator]],
      Labour11: ['', [Validators.required, DecimalValidator]],
      Labour12: ['', [Validators.required, DecimalValidator]],
      Labour13: ['', [Validators.required, DecimalValidator]],
      Labour14: ['', [Validators.required, DecimalValidator]],
      Labour15: ['', [Validators.required, DecimalValidator]],
      Labour16: ['', [Validators.required, DecimalValidator]],
      Labour17: ['', [Validators.required, DecimalValidator]],
      Labour18: ['', [Validators.required, DecimalValidator]],
      Labour19: ['', [Validators.required, DecimalValidator]],
      Labour20: ['', [Validators.required, DecimalValidator]],
    });
  }
}
