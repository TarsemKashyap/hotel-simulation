import { Injectable } from '@angular/core';
import { AppRoles } from '../public/account';
import { RolePagesDtl, StudentRoles } from '../shared/class/model/Roles';
import { JsonPipe } from '@angular/common';
import { DefaultTitleStrategy } from '@angular/router';
import { DefaultClassSession } from '../shared/class';
import { StudentDecision } from '../shared/class/model/decision.model';

const RefreshToken = 'RefreshToken';
const AccessToken = 'AccessToken';
const userRole = 'userRole';
const studentRole = 'studentRole';
const currentRole = 'currentRole';
const studentDefaultClass = 'studentDefaultClass';
const UrlClassId = 'urlClassId';
const INST_STUDENT_DECISION="inst-student-decision"

const manageClass = {
  pageKey: 'rmChangeClass',
  pageName: 'Manage classes',
  roleName: [1, 2, 3, 4, 5, 6],
  childPageLink: 'change-class',
  icon: 'school',
};

const decision = {
  pageKey: 'rmMakeDecision',
  pageName: 'Enter your decisions',
  roleName: [1, 4, 2, 3, 6, 5],
  childPageLink: 'decision',
  icon: 'signpost',
};

const studentRoutes: RolePagesDtl[] = [
  manageClass,
  decision,
  {
    pageKey: 'gmSetYourObjective',
    pageName: 'Set your objectives',
    roleName: [4],
    childPageLink: 'goalSetting',
    icon: 'star',
  },
  {
    pageKey: 'rmLoan',
    pageName: 'Borrow/Pay loans',
    roleName: [1, 4, 2, 3, 6, 5],
    childPageLink: 'loan',
    icon: 'paid',
  },
];

const noRoles: RolePagesDtl[] = [manageClass];

@Injectable({ providedIn: 'root' })
export class SessionStore {
  constructor() {}

  SetRefreshToken(value: string) {
    localStorage.setItem(RefreshToken, value);
  }

  GetRefreshToken() {
    return localStorage.getItem(RefreshToken);
  }

  SetStudentRole(value: StudentRoles[]) {
    localStorage.setItem(studentRole, JSON.stringify(value));
  }

  GetRoleids(): number[] {
    const stdRoles = this.GetStudentRoleList();
    return stdRoles.map((x) => x.id);
  }

  SetDefaultClass(defaultclass: DefaultClassSession) {
    localStorage.setItem(studentDefaultClass, JSON.stringify(defaultclass));
  }
  removeDefaultClass() {
    localStorage.removeItem(studentDefaultClass);
  }
  GetDefaultClass(): DefaultClassSession | undefined {
    const cls = localStorage.getItem(studentDefaultClass);
    if (cls) {
      return JSON.parse(cls);
    }
    return undefined;
  }


  GetStudentRoleList() {
    let savedRoles = localStorage.getItem(studentRole);
    var selectedRolesArr: StudentRoles[] = savedRoles
      ? JSON.parse(savedRoles)
      : [];
    return selectedRolesArr;
  }

  GetStudentRoutes() {
    var selectedRolesArr = this.GetStudentRoleList();
    if (!selectedRolesArr.length) {
      return noRoles;
    }

    const roleArray = studentRoutes.filter((route: RolePagesDtl) => {
      return selectedRolesArr.some((p) => {
        const rl = route.roleName.some((r) => r == p.id);
        return rl;
      });
    });
    return roleArray;
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

  SetStudentDecision(data:StudentDecision){
    sessionStorage.setItem(INST_STUDENT_DECISION, JSON.stringify(data));
  }
  getStudentDecision(){
    const std= sessionStorage.getItem(INST_STUDENT_DECISION);
    if(std){
      return JSON.parse(std);
    }
    throw new Error(`No value found for Key ${INST_STUDENT_DECISION} in session storage`)
  }
}
