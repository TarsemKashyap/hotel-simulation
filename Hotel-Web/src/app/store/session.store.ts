import { Injectable } from '@angular/core';
import { AppRoles } from '../public/account';
import {RolePagesDtl}  from '../shared/class/model/Roles';

const RefreshToken = 'RefreshToken';
const AccessToken = 'AccessToken';
const userRole = 'userRole';
const revenueManagerPagesArr: RolePagesDtl[] = [];
const generalManagerPagesArr: RolePagesDtl[] = [];
const roManagerPagesArr: RolePagesDtl[] = [];
const fbManagerPagesArr: RolePagesDtl[] = [];
let studentRole = 'studentRole';
let currentRole = 'currentRole';
revenueManagerPagesArr.push({pageKey:"rmChangeClass",pageName:"Add/Change class",roleName:"RM"},{pageKey:"rmMakeDecision",pageName:"Make your decisions",roleName:"RM"},{pageKey:"rmViewReport",pageName:"viewReports",roleName:"RM"},{pageKey:"rmLoan",pageName:"Borrow/Pay your loans",roleName:"RM"});
generalManagerPagesArr.push({pageKey:"gmChangeClass",pageName:"Add/Change class",roleName:"GM"},{pageKey:"gmSetYourObjective",pageName:"Set your objective",roleName:"GM"},{pageKey:"rmMakeDecision",pageName:"Make your decisions",roleName:"GM"},{pageKey:"gmViewReport",pageName:"viewReports",roleName:"GM"},{pageKey:"gmLoan",pageName:"Borrow/Pay your loans",roleName:"GM"});
roManagerPagesArr.push({pageKey:"roChangeClass",pageName:"Add/Change class",roleName:"RO"},{pageKey:"roMakeDecision",pageName:"Make your decisions",roleName:"RO"},{pageKey:"roViewReport",pageName:"viewReports",roleName:"RO"},{pageKey:"roLoan",pageName:"Borrow/Pay your loans",roleName:"RO"});
fbManagerPagesArr.push({pageKey:"fbChangeClass",pageName:"Add/Change class",roleName:"FB"},{pageKey:"fbMakeDecision",pageName:"Make your decisions",roleName:"FB"},{pageKey:"fbViewReport",pageName:"viewReports",roleName:"FB"},{pageKey:"fbLoan",pageName:"Borrow/Pay your loans",roleName:"FB"});

let RolesDetails = new Map<number, RolePagesDtl[]>([
  [1, revenueManagerPagesArr],
  [3, fbManagerPagesArr],
  [4, generalManagerPagesArr],
  [5, roManagerPagesArr],
 
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

  SetStudentRole(value:any) {
    localStorage.setItem(studentRole, JSON.stringify(value));
  }

  SetCurrentRole(value:any){
    localStorage.setItem(currentRole, value);
  }

  GetCurrentRole () {
    return localStorage.getItem(currentRole) 
  }

  studentAssignRoleList() {
    return localStorage.getItem(studentRole) ;
  }

  GetStudentRole() {
    var rolesArray : string = "";
    var selectedRolesArr = JSON.parse(localStorage.getItem(studentRole) || '[]');
    const roleArray: any = [];
    selectedRolesArr.forEach((element : any) => {
      let arrDtl = RolesDetails.get(element.id) == undefined?[]:RolesDetails.get(element.id) ;
      roleArray.push(...arrDtl ? arrDtl : []);
    });
    rolesArray += JSON.stringify(roleArray);
    return rolesArray;
  }

  SetAccessToken(value: string) {
    localStorage.setItem(AccessToken, value);
  }

  GetAccessToken() {
    return localStorage.getItem(AccessToken);
  }

  AddRole(role: AppRoles[]) {
    const json = JSON.stringify(role);
    localStorage.setItem(userRole, json);
  }
  GetRole(): AppRoles[] {
    var data = localStorage.getItem(userRole);
    if (data) {
      return JSON.parse(data) as AppRoles[];
    }
    return [];
  }

  clearSession() {
    return localStorage.clear();
  }

 
}
