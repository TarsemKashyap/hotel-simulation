import { Injectable } from '@angular/core';
import { AppRoles } from '../public/account';
import {RolePagesDtl}  from '../shared/class/model/Roles';

const RefreshToken = 'RefreshToken';
const AccessToken = 'AccessToken';
const userRole = 'userRole';
const revenueManagerPagesArr: RolePagesDtl[] = [];
let studentRole = 'studentRole';
revenueManagerPagesArr.push({pageKey:"rmChangeClass",pageName:"Add/Change class",roleName:"RM"},{pageKey:"rmMakeDecision",pageName:"makeDecisions",roleName:"RM"},{pageKey:"rmViewReport",pageName:"viewReports",roleName:"RM"},{pageKey:"rmLoan",pageName:"loan",roleName:"RM"});

let RolesDetails = new Map<number, RolePagesDtl[]>([
  [1, revenueManagerPagesArr],
]);

@Injectable({ providedIn: 'root' })
export class SessionStore {
  constructor() {}

  SetRefreshToken(value: string) {
    localStorage.setItem(RefreshToken, value);
  }

  GetRefreshToken() {
    return localStorage.getItem(RefreshToken);
  }

  SetStudentRole(value:any){
    localStorage.setItem(studentRole, JSON.stringify(value));
  }

  GetStudentRole() {
    var rolesArray : string="";
    var selectedRolesArr = JSON.parse(localStorage.getItem(studentRole) || '[]');
    selectedRolesArr.forEach((element : any) => {
      let arrDtl = RolesDetails.get(element.id) == undefined?[]:RolesDetails.get(element.id) ;
      rolesArray += JSON.stringify(arrDtl);
    });
    return rolesArray;
  }

  SetAccessToken(value: string) {
    localStorage.setItem(AccessToken, value);
  }

  GetAccessToken() {
    return localStorage.getItem(AccessToken);
  }

  AddRole(role: AppRoles) {
    localStorage.setItem(userRole, role);
  }
  GetRole(){
    return localStorage.getItem(userRole);
  }

 
}
