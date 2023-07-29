import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from '../student.service';
import { AttributeDecision } from 'src/app/shared/class/model/classSession.model';
import { SessionStore } from 'src/app/store';
import { Router } from '@angular/router';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.css']
})
export class AttributeComponent {
  form: FormGroup;
  submitted = false;
  totalAccumulated: string = "0";
  totalAmenities: string = "0";
  totalOther: string = "0";
  totalLabour: string = "0";
  totalExpensesAllocated = "0";
  attributeDecisions: AttributeDecision[] = [];
  selectedRoles: any = [];
  selectedRoleList: any = [];
  currentRole: any = '';

  ngOnInit(): void {
    this.attributeDecisionList();
  }

  constructor(
    private studentService: StudentService, private fb: FormBuilder, private sessionStore: SessionStore, private router: Router) {
    this.form = this.createForm();
    this.currentRole = this.sessionStore.GetCurrentRole();
    if (this.currentRole === undefined || this.currentRole === '') {
      this.router.navigate(['']);
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  sum() {

    var NSubTotal;
    var BSubTotal;
    var LSubTotal;
    var sum = parseFloat((this.form.value.Amenities1 === undefined || this.form.value.Amenities1 === '') ? 0 : this.form.value.Amenities1.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities2 === undefined || this.form.value.Amenities2 === '') ? 0 : this.form.value.Amenities2.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities3 === undefined || this.form.value.Amenities3 === '') ? 0 : this.form.value.Amenities3.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities4 === undefined || this.form.value.Amenities4 === '') ? 0 : this.form.value.Amenities4.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities5 === undefined || this.form.value.Amenities5 === '') ? 0 : this.form.value.Amenities5.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities6 === undefined || this.form.value.Amenities6 === '') ? 0 : this.form.value.Amenities6.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities7 === undefined || this.form.value.Amenities7 === '') ? 0 : this.form.value.Amenities7.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities8 === undefined || this.form.value.Amenities8 === '') ? 0 : this.form.value.Amenities8.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities9 === undefined || this.form.value.Amenities9 === '') ? 0 : this.form.value.Amenities9.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities10 === undefined || this.form.value.Amenities10 === '') ? 0 : this.form.value.Amenities10.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities11 === undefined || this.form.value.Amenities11 === '') ? 0 : this.form.value.Amenities11.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities12 === undefined || this.form.value.Amenities12 === '') ? 0 : this.form.value.Amenities12.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities13 === undefined || this.form.value.Amenities13 === '') ? 0 : this.form.value.Amenities13.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities14 === undefined || this.form.value.Amenities14 === '') ? 0 : this.form.value.Amenities14.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities15 === undefined || this.form.value.Amenities15 === '') ? 0 : this.form.value.Amenities15.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities16 === undefined || this.form.value.Amenities16 === '') ? 0 : this.form.value.Amenities16.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities17 === undefined || this.form.value.Amenities17 === '') ? 0 : this.form.value.Amenities17.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities18 === undefined || this.form.value.Amenities18 === '') ? 0 : this.form.value.Amenities18.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities19 === undefined || this.form.value.Amenities19 === '') ? 0 : this.form.value.Amenities19.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Amenities20 === undefined || this.form.value.Amenities20 === '') ? 0 : this.form.value.Amenities20.toString().replace(",", ""));
    NSubTotal = sum;
    this.totalAmenities = NSubTotal.toString();
    sum = sum + parseFloat((this.form.value.Other1 === undefined || this.form.value.Other1 === '') ? 0 : this.form.value.Other1.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other2 === undefined || this.form.value.Other2 === '') ? 0 : this.form.value.Other2.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other3 === undefined || this.form.value.Other3 === '') ? 0 : this.form.value.Other3.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other4 === undefined || this.form.value.Other4 === '') ? 0 : this.form.value.Other4.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other5 === undefined || this.form.value.Other5 === '') ? 0 : this.form.value.Other5.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other6 === undefined || this.form.value.Other6 === '') ? 0 : this.form.value.Other6.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other7 === undefined || this.form.value.Other7 === '') ? 0 : this.form.value.Other7.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other8 === undefined || this.form.value.Other8 === '') ? 0 : this.form.value.Other8.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other9 === undefined || this.form.value.Other9 === '') ? 0 : this.form.value.Other9.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other10 === undefined || this.form.value.Other10 === '') ? 0 : this.form.value.Other10.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other11 === undefined || this.form.value.Other11 === '') ? 0 : this.form.value.Other11.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other12 === undefined || this.form.value.Other12 === '') ? 0 : this.form.value.Other12.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other13 === undefined || this.form.value.Other13 === '') ? 0 : this.form.value.Other13.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other14 === undefined || this.form.value.Other14 === '') ? 0 : this.form.value.Other14.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other15 === undefined || this.form.value.Other15 === '') ? 0 : this.form.value.Other15.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other16 === undefined || this.form.value.Other16 === '') ? 0 : this.form.value.Other16.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other17 === undefined || this.form.value.Other17 === '') ? 0 : this.form.value.Other17.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other18 === undefined || this.form.value.Other18 === '') ? 0 : this.form.value.Other18.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other19 === undefined || this.form.value.Other19 === '') ? 0 : this.form.value.Other19.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Other20 === undefined || this.form.value.Other20 === '') ? 0 : this.form.value.Other20.toString().replace(",", ""));
    BSubTotal = sum - NSubTotal;
    this.totalOther = BSubTotal.toString();
    sum = sum + parseFloat((this.form.value.Labour1 === undefined || this.form.value.Labour1 === '') ? 0 : this.form.value.Labour1.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour2 === undefined || this.form.value.Labour2 === '') ? 0 : this.form.value.Labour2.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour3 === undefined || this.form.value.Labour3 === '') ? 0 : this.form.value.Labour3.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour4 === undefined || this.form.value.Labour4 === '') ? 0 : this.form.value.Labour4.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour5 === undefined || this.form.value.Labour5 === '') ? 0 : this.form.value.Labour5.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour6 === undefined || this.form.value.Labour6 === '') ? 0 : this.form.value.Labour6.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour7 === undefined || this.form.value.Labour7 === '') ? 0 : this.form.value.Labour7.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour8 === undefined || this.form.value.Labour8 === '') ? 0 : this.form.value.Labour8.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour9 === undefined || this.form.value.Labour9 === '') ? 0 : this.form.value.Labour9.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour10 === undefined || this.form.value.Labour10 === '') ? 0 : this.form.value.Labour10.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour11 === undefined || this.form.value.Labour11 === '') ? 0 : this.form.value.Labour11.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour12 === undefined || this.form.value.Labour12 === '') ? 0 : this.form.value.Labour12.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour13 === undefined || this.form.value.Labour13 === '') ? 0 : this.form.value.Labour13.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour14 === undefined || this.form.value.Labour14 === '') ? 0 : this.form.value.Labour14.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour15 === undefined || this.form.value.Labour15 === '') ? 0 : this.form.value.Labour15.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour16 === undefined || this.form.value.Labour16 === '') ? 0 : this.form.value.Labour16.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour17 === undefined || this.form.value.Labour17 === '') ? 0 : this.form.value.Labour17.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour18 === undefined || this.form.value.Labour18 === '') ? 0 : this.form.value.Labour18.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour19 === undefined || this.form.value.Labour19 === '') ? 0 : this.form.value.Labour19.toString().replace(",", ""));
    sum = sum + parseFloat((this.form.value.Labour20 === undefined || this.form.value.Labour20 === '') ? 0 : this.form.value.Labour20.toString().replace(",", ""));
    LSubTotal = sum - NSubTotal - BSubTotal;
    this.totalLabour = LSubTotal.toString();
    this.totalExpensesAllocated = sum.toString();
  }

  private attributeDecisionList() {
    this.studentService
      .AttributeDecisionList().subscribe((data) => {
        this.attributeDecisions = data;
        var spaAttribute = this.attributeDecisions.find(d => d.attribute === 'Spa');

        var fitnessCenterAttribute = this.attributeDecisions.find(d => d.attribute === 'Fitness Center');
        
        var buisnessCenterAttribute = this.attributeDecisions.find(d => d.attribute === 'Business Center');

        var golfCourseAttribute = this.attributeDecisions.find(d => d.attribute === 'Golf Course');

        var recreationFacilitiesAttribute = this.attributeDecisions.find(d => d.attribute === 'Other Recreation Facilities - Pools, game rooms, tennis courts, ect');

        var salesAttentionAttribute = this.attributeDecisions.find(d => d.attribute === 'Management/Sales Attention');

        var resturantsAttribute = this.attributeDecisions.find(d => d.attribute === 'Management/Sales Attention');

        var barsAttribute = this.attributeDecisions.find(d => d.attribute === 'Bars');

        var roomServiceAttribute = this.attributeDecisions.find(d => d.attribute === 'Room Service');

        var banquetAttribute = this.attributeDecisions.find(d => d.attribute === 'Banquet & Catering');

        var meetingRoomsAttribute = this.attributeDecisions.find(d => d.attribute === 'Meeting Rooms');

        var entertainmentAttribute = this.attributeDecisions.find(d => d.attribute === 'Entertainment');

        var courtesyAttribute = this.attributeDecisions.find(d => d.attribute === 'Courtesy(FB)');

        var guestRoomsAttribute = this.attributeDecisions.find(d => d.attribute === 'Guest Rooms');

        var reservationsAttribute = this.attributeDecisions.find(d => d.attribute === 'Reservations');

        var guestCheckAttribute = this.attributeDecisions.find(d => d.attribute === 'Guest Check in/Guest Check out');

        var conciergeAttribute = this.attributeDecisions.find(d => d.attribute === 'Concierge');

        var housekeepingAttribute = this.attributeDecisions.find(d => d.attribute === 'Housekeeping');

        var maintanenceAttribute = this.attributeDecisions.find(d => d.attribute === 'Maintanence and security');

        var courtesyRoomsAttribute = this.attributeDecisions.find(d => d.attribute === 'Courtesy (Rooms)');

        this.form.patchValue({ Accumulated1: spaAttribute?.accumulatedCapital, Amenities1: spaAttribute?.newCapital, Other1: spaAttribute?.operationBudget, Labour1: spaAttribute?.laborBudget, Accumulated2: fitnessCenterAttribute?.accumulatedCapital, Amenities2: fitnessCenterAttribute?.newCapital, Other2: fitnessCenterAttribute?.operationBudget, Labour2: fitnessCenterAttribute?.laborBudget, Accumulated3: buisnessCenterAttribute?.accumulatedCapital, Amenities3: buisnessCenterAttribute?.newCapital, Other3: buisnessCenterAttribute?.operationBudget, Labour3: buisnessCenterAttribute?.laborBudget, Accumulated4: golfCourseAttribute?.accumulatedCapital, Amenities4: golfCourseAttribute?.newCapital, Other4: golfCourseAttribute?.operationBudget, Labour4: golfCourseAttribute?.laborBudget, Accumulated5: recreationFacilitiesAttribute?.accumulatedCapital, Amenities5: recreationFacilitiesAttribute?.newCapital, Other5: recreationFacilitiesAttribute?.operationBudget, Labour5: recreationFacilitiesAttribute?.laborBudget, Accumulated6: salesAttentionAttribute?.accumulatedCapital, Amenities6: salesAttentionAttribute?.newCapital, Other6: salesAttentionAttribute?.operationBudget, Labour6: salesAttentionAttribute?.laborBudget, Accumulated7: resturantsAttribute?.accumulatedCapital, Amenities7: resturantsAttribute?.newCapital, Other7: resturantsAttribute?.operationBudget, Labour7: resturantsAttribute?.laborBudget, Accumulated8: barsAttribute?.accumulatedCapital, Amenities8: barsAttribute?.newCapital, Other8: barsAttribute?.operationBudget, Labour8: barsAttribute?.laborBudget, Accumulated9: roomServiceAttribute?.accumulatedCapital, Amenities9: roomServiceAttribute?.newCapital, Other9: roomServiceAttribute?.operationBudget, Labour9: roomServiceAttribute?.laborBudget, Accumulated10: banquetAttribute?.accumulatedCapital, Amenities10: banquetAttribute?.newCapital, Other10: banquetAttribute?.operationBudget, Labour10: banquetAttribute?.laborBudget, Accumulated11: meetingRoomsAttribute?.accumulatedCapital, Amenities11: meetingRoomsAttribute?.newCapital, Other11: meetingRoomsAttribute?.operationBudget, Labour11: meetingRoomsAttribute?.laborBudget, Accumulated12: entertainmentAttribute?.accumulatedCapital, Amenities12: entertainmentAttribute?.newCapital, Other12: entertainmentAttribute?.operationBudget, Labour12: entertainmentAttribute?.laborBudget, Accumulated13: courtesyAttribute?.accumulatedCapital, Amenities13: courtesyAttribute?.newCapital, Other13: courtesyAttribute?.operationBudget, Labour13: courtesyAttribute?.laborBudget, Accumulated14: guestRoomsAttribute?.accumulatedCapital, Amenities14: guestRoomsAttribute?.newCapital, Other14: guestRoomsAttribute?.operationBudget, Labour14: guestRoomsAttribute?.laborBudget, Accumulated15: reservationsAttribute?.accumulatedCapital, Amenities15: reservationsAttribute?.newCapital, Other15: reservationsAttribute?.operationBudget, Labour15: reservationsAttribute?.laborBudget, Accumulated16: guestCheckAttribute?.accumulatedCapital, Amenities16: guestCheckAttribute?.newCapital, Other16: guestCheckAttribute?.operationBudget, Labour16: guestCheckAttribute?.laborBudget, Accumulated17: conciergeAttribute?.accumulatedCapital, Amenities17: conciergeAttribute?.newCapital, Other17: conciergeAttribute?.operationBudget, Labour17: conciergeAttribute?.laborBudget, Accumulated18: housekeepingAttribute?.accumulatedCapital, Amenities18: housekeepingAttribute?.newCapital, Other18: housekeepingAttribute?.operationBudget, Labour18: housekeepingAttribute?.laborBudget, Accumulated19: maintanenceAttribute?.accumulatedCapital, Amenities19: maintanenceAttribute?.newCapital, Other19: maintanenceAttribute?.operationBudget, Labour19: maintanenceAttribute?.laborBudget, Accumulated20: courtesyRoomsAttribute?.accumulatedCapital, Amenities20: courtesyRoomsAttribute?.newCapital, Other20: courtesyRoomsAttribute?.operationBudget, Labour20: courtesyRoomsAttribute?.laborBudget });
        switch(this.currentRole) {
          case "RO": {
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
            break;
          }
          case "FB": {
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
            break;
          }
          case "RM": {
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
            break;
          }
        }
        
        
        var totalAccumu = 0;
        console.log(spaAttribute, fitnessCenterAttribute)
        totalAccumu = parseFloat(spaAttribute?.accumulatedCapital === undefined ? '0' : spaAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(fitnessCenterAttribute?.accumulatedCapital === undefined ? '0' : fitnessCenterAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(banquetAttribute?.accumulatedCapital === undefined ? '0' : banquetAttribute?.accumulatedCapital.toString());

        totalAccumu = totalAccumu + parseFloat(buisnessCenterAttribute?.accumulatedCapital === undefined ? '0' : buisnessCenterAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(golfCourseAttribute?.accumulatedCapital === undefined ? '0' : golfCourseAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(recreationFacilitiesAttribute?.accumulatedCapital === undefined ? '0' : recreationFacilitiesAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(salesAttentionAttribute?.accumulatedCapital === undefined ? '0' : salesAttentionAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(resturantsAttribute?.accumulatedCapital === undefined ? '0' : resturantsAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(barsAttribute?.accumulatedCapital === undefined ? '0' : barsAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(roomServiceAttribute?.accumulatedCapital === undefined ? '0' : roomServiceAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(banquetAttribute?.accumulatedCapital === undefined ? '0' : banquetAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(meetingRoomsAttribute?.accumulatedCapital === undefined ? '0' : meetingRoomsAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(entertainmentAttribute?.accumulatedCapital === undefined ? '0' : entertainmentAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(entertainmentAttribute?.accumulatedCapital === undefined ? '0' : entertainmentAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(guestRoomsAttribute?.accumulatedCapital === undefined ? '0' : guestRoomsAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(reservationsAttribute?.accumulatedCapital === undefined ? '0' : reservationsAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(guestCheckAttribute?.accumulatedCapital === undefined ? '0' : guestCheckAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(conciergeAttribute?.accumulatedCapital === undefined ? '0' : conciergeAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(housekeepingAttribute?.accumulatedCapital === undefined ? '0' : housekeepingAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(maintanenceAttribute?.accumulatedCapital === undefined ? '0' : maintanenceAttribute?.accumulatedCapital.toString());
        totalAccumu = totalAccumu + parseFloat(courtesyRoomsAttribute?.accumulatedCapital === undefined ? '0' : courtesyRoomsAttribute?.accumulatedCapital.toString());
        this.totalAccumulated = totalAccumu.toString();
        this.sum();
      });
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.attributeDecisions.forEach(element => {
      switch (element.attribute) {
        case "Spa":
          {
            element.newCapital = this.form.value.Amenities1;
            element.operationBudget = this.form.value.Other1;
            element.laborBudget = this.form.value.Labour1;
            break;
          }
        case "Fitness Center":
          {
            element.newCapital = this.form.value.Amenities2;
            element.operationBudget = this.form.value.Other2;
            element.laborBudget = this.form.value.Labour2;
            break;
          }
        case "Business Center":
          {
            element.newCapital = this.form.value.Amenities3;
            element.operationBudget = this.form.value.Other3;
            element.laborBudget = this.form.value.Labour3;
            break;
          }
        case "Golf Course":
          {
            element.newCapital = this.form.value.Amenities4;
            element.operationBudget = this.form.value.Other4;
            element.laborBudget = this.form.value.Labour4;
            break;
          }
        case "Other Recreation Facilities - Pools, game rooms, tennis courts, ect":
          {
            element.newCapital = this.form.value.Amenities5;
            element.operationBudget = this.form.value.Other5;
            element.laborBudget = this.form.value.Labour5;
            break;
          }
        case "Management/Sales Attention":
          {
            element.newCapital = this.form.value.Amenities6;
            element.operationBudget = this.form.value.Other6;
            element.laborBudget = this.form.value.Labour6;
            break;
          }
        case "Resturants":
          {
            element.newCapital = this.form.value.Amenities7;
            element.operationBudget = this.form.value.Other7;
            element.laborBudget = this.form.value.Labour7;
            break;
          }
        case "Bars":
          {
            element.newCapital = this.form.value.Amenities8;
            element.operationBudget = this.form.value.Other8;
            element.laborBudget = this.form.value.Labour8;
            break;
          }
        case "Room Service":
          {
            element.newCapital = this.form.value.Amenities9;
            element.operationBudget = this.form.value.Other9;
            element.laborBudget = this.form.value.Labour9;
            break;
          }
        case "Banquet & Catering":
          {
            element.newCapital = this.form.value.Amenities10;
            element.operationBudget = this.form.value.Other10;
            element.laborBudget = this.form.value.Labour10;
            break;
          }
        case "Meeting Rooms":
          {
            element.newCapital = this.form.value.Amenities11;
            element.operationBudget = this.form.value.Other11;
            element.laborBudget = this.form.value.Labour11;
            break;
          }
        case "Entertainment":
          {
            element.newCapital = this.form.value.Amenities12;
            element.operationBudget = this.form.value.Other12;
            element.laborBudget = this.form.value.Labour12;
            break;
          }
        case "Courtesy(FB)":
          {
            element.newCapital = this.form.value.Amenities13;
            element.operationBudget = this.form.value.Other13;
            element.laborBudget = this.form.value.Labour13;
            break;
          }
        case "Guest Rooms":
          {
            element.newCapital = this.form.value.Amenities14;
            element.operationBudget = this.form.value.Other14;
            element.laborBudget = this.form.value.Labour14;
            break;
          }
        case "Reservations":
          {
            element.newCapital = this.form.value.Amenities15;
            element.operationBudget = this.form.value.Other15;
            element.laborBudget = this.form.value.Labour15;
            break;
          }

        case "Guest Check in/Guest Check out":
          {
            element.newCapital = this.form.value.Amenities16;
            element.operationBudget = this.form.value.Other16;
            element.laborBudget = this.form.value.Labour16;
            break;
          }
        case "Concierge":
          {
            element.newCapital = this.form.value.Amenities17;
            element.operationBudget = this.form.value.Other17;
            element.laborBudget = this.form.value.Labour17;
            break;
          }
        case "Housekeeping":
          {
            element.newCapital = this.form.value.Amenities18;
            element.operationBudget = this.form.value.Other18;
            element.laborBudget = this.form.value.Labour18;
            break;
          }
        case "Maintanence and security":
          {
            element.newCapital = this.form.value.Amenities19;
            element.operationBudget = this.form.value.Other19;
            element.laborBudget = this.form.value.Labour19;
            break;
          }
        case "Courtesy (Rooms)":
          {
            element.newCapital = this.form.value.Amenities20;
            element.operationBudget = this.form.value.Other20;
            element.laborBudget = this.form.value.Labour20;
            break;
          }
      }
    });
    this.studentService.AttributeDecisionUpdate(this.attributeDecisions).subscribe((x) => {
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
      Amenities1: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities2: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities3: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities4: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities5: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities6: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities7: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities8: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities9: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities10: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities11: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities12: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities13: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities14: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities15: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities16: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities17: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities18: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities19: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Amenities20: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other1: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other2: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other3: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other4: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other5: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other6: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other7: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other8: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other9: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other10: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other11: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other12: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other13: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other14: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other15: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other16: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other17: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other18: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other19: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Other20: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour1: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour2: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour3: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour4: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour5: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour6: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour7: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour8: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour9: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour10: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour11: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour12: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour13: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour14: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour15: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour16: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour17: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour18: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour19: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
      Labour20: ['', [Validators.required, Validators.pattern("[0-9]+[.,]?[0-9]*")]],
    });
  }
}
