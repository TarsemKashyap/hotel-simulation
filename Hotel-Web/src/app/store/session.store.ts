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
const marketManagerPagesArr: RolePagesDtl[] = [];
const retailManagerPagesArr: RolePagesDtl[] = [];
let studentRole = 'studentRole';
let currentRole = 'currentRole';
revenueManagerPagesArr.push({pageKey:"rmChangeClass",pageName:"Add/Change class",roleName:"RM",childPageLink:"change-class"},{pageKey:"rmMakeDecision",pageName:"Make your decisions",roleName:"RM",childPageLink:"decision"},{pageKey:"rmViewReport",pageName:"viewReports",roleName:"RM",childPageLink:""},{pageKey:"rmLoan",pageName:"Borrow/Pay your loans",roleName:"RM",childPageLink:""});
generalManagerPagesArr.push({pageKey:"gmChangeClass",pageName:"Add/Change class",roleName:"GM",childPageLink:"change-class"},{pageKey:"gmSetYourObjective",pageName:"Set your objective",roleName:"GM",childPageLink:""},{pageKey:"rmMakeDecision",pageName:"Make your decisions",roleName:"GM",childPageLink:"decision"},{pageKey:"gmViewReport",pageName:"viewReports",roleName:"GM",childPageLink:""},{pageKey:"gmLoan",pageName:"Borrow/Pay your loans",roleName:"GM",childPageLink:""});
roManagerPagesArr.push({pageKey:"roChangeClass",pageName:"Add/Change class",roleName:"RO",childPageLink:"change-class"},{pageKey:"roMakeDecision",pageName:"Make your decisions",roleName:"RO",childPageLink:"decision"},{pageKey:"roViewReport",pageName:"viewReports",roleName:"RO",childPageLink:""},{pageKey:"roLoan",pageName:"Borrow/Pay your loans",roleName:"RO",childPageLink:""});
fbManagerPagesArr.push({pageKey:"fbChangeClass",pageName:"Add/Change class",roleName:"FB",childPageLink:"change-class"},{pageKey:"fbMakeDecision",pageName:"Make your decisions",roleName:"FB",childPageLink:"decision"},{pageKey:"fbViewReport",pageName:"viewReports",roleName:"FB",childPageLink:""},{pageKey:"fbLoan",pageName:"Borrow/Pay your loans",roleName:"FB",childPageLink:""});
marketManagerPagesArr.push({pageKey:"mmChangeClass",pageName:"Add/Change class",roleName:"MM",childPageLink:"change-class"},{pageKey:"mmMakeDecision",pageName:"Make your decisions",roleName:"MM",childPageLink:"decision"},{pageKey:"mmViewReport",pageName:"viewReports",roleName:"MM",childPageLink:""},{pageKey:"mmLoan",pageName:"Borrow/Pay your loans",roleName:"MM",childPageLink:""});
retailManagerPagesArr.push({pageKey:"retailChangeClass",pageName:"Add/Change class",roleName:"Retail",childPageLink:"change-class"},{pageKey:"retailMakeDecision",pageName:"Make your decisions",roleName:"Retail",childPageLink:"decision"},{pageKey:"retailViewReport",pageName:"viewReports",roleName:"Retail",childPageLink:""},{pageKey:"retailLoan",pageName:"Borrow/Pay your loans",roleName:"Retail",childPageLink:""});

let RolesDetails = new Map<number, RolePagesDtl[]>([
  [1, revenueManagerPagesArr],
  [2,retailManagerPagesArr],
  [3, fbManagerPagesArr],
  [4, generalManagerPagesArr],
  [5, roManagerPagesArr],
  [6, roManagerPagesArr],
 
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
