import { Injectable } from '@angular/core';
import { AppRoles } from '../public/account';
import { RolePagesDtl } from '../shared/class/model/Roles';
import { JsonPipe } from '@angular/common';

const RefreshToken = 'RefreshToken';
const AccessToken = 'AccessToken';
const userRole = 'userRole';
const revenueManagerPagesArr: RolePagesDtl[] = [];
const generalManagerPagesArr: RolePagesDtl[] = [];
const roManagerPagesArr: RolePagesDtl[] = [];
const fbManagerPagesArr: RolePagesDtl[] = [];
const marketManagerPagesArr: RolePagesDtl[] = [];
const roomManagerPagesArr: RolePagesDtl[] = [];
const noRoles: RolePagesDtl[] = [];
let studentRole = 'studentRole';
let currentRole = 'currentRole';

revenueManagerPagesArr.push(
  {
    pageKey: 'rmChangeClass',
    pageName: 'Class Overview',
    roleName: 'RM',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'rmMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'RM',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'rmViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'RM',
  //   childPageLink: '',
  // },
  {
    pageKey: 'rmLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'RM',
    childPageLink: 'loan',
  }
);
generalManagerPagesArr.push(
  {
    pageKey: 'gmChangeClass',
    pageName: 'Class Overview',
    roleName: 'GM',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'gmSetYourObjective',
    pageName: 'Set your objective',
    roleName: 'GM',
    childPageLink: 'goalSetting',
  },
  {
    pageKey: 'rmMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'GM',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'gmViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'GM',
  //   childPageLink: '',
  // },
  {
    pageKey: 'gmLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'GM',
    childPageLink: 'loan',
  }
);
roManagerPagesArr.push(
  {
    pageKey: 'roChangeClass',
    pageName: 'Class Overview',
    roleName: 'RT',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'roMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'RT',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'roViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'RT',
  //   childPageLink: '',
  // },
  {
    pageKey: 'roLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'RT',
    childPageLink: 'loan',
  }
);
fbManagerPagesArr.push(
  {
    pageKey: 'fbChangeClass',
    pageName: 'Class Overview',
    roleName: 'FB',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'fbMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'FB',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'fbViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'FB',
  //   childPageLink: '',
  // },
  {
    pageKey: 'fbLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'FB',
    childPageLink: 'loan',
  }
);
marketManagerPagesArr.push(
  {
    pageKey: 'mmChangeClass',
    pageName: 'Class Overview',
    roleName: 'MM',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'mmMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'MM',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'mmViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'MM',
  //   childPageLink: '',
  // },
  {
    pageKey: 'mmLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'MM',
    childPageLink: 'loan',
  }
);
roomManagerPagesArr.push(
  {
    pageKey: 'retailChangeClass',
    pageName: 'Class Overview',
    roleName: 'RO',
    childPageLink: 'change-class',
  },
  {
    pageKey: 'retailMakeDecision',
    pageName: 'Make your decisions',
    roleName: 'RO',
    childPageLink: 'decision',
  },
  // {
  //   pageKey: 'retailViewReport',
  //   pageName: 'viewReports',
  //   roleName: 'RO',
  //   childPageLink: '',
  // },
  {
    pageKey: 'retailLoan',
    pageName: 'Borrow/Pay your loans',
    roleName: 'RO',
    childPageLink: 'loan',
  }
);
noRoles.push(
  {
    pageKey: 'ChangePwd',
    pageName: 'Change password',
    roleName: '',
    childPageLink: 'change-password',
  },
  {
    pageKey: 'noRoleChangeClass',
    pageName: 'Class Overview',
    roleName: '',
    childPageLink: 'change-class',
  }
);

let RolesDetails = new Map<number, RolePagesDtl[]>([
  [1, revenueManagerPagesArr],
  [2, roManagerPagesArr],
  [3, fbManagerPagesArr],
  [4, generalManagerPagesArr],
  [5, roomManagerPagesArr],
  [6, marketManagerPagesArr],
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

  SetStudentRole(value: any) {
    localStorage.setItem(studentRole, JSON.stringify(value));
  }

  SetCurrentRole(value: any) {
    localStorage.setItem(currentRole, value);
  }

  GetCurrentRole() {
    return localStorage.getItem(currentRole);
  }

  studentAssignRoleList() {
    return localStorage.getItem(studentRole);
  }

  GetStudentRole() {
    var selectedRolesArr = JSON.parse(localStorage.getItem(studentRole) || '');
    if (!selectedRolesArr.length) {
      return noRoles;
    }
    const roleArray: RolePagesDtl[] = [];
    selectedRolesArr.forEach((element: any) => {
      let arrDtl =
        RolesDetails.get(element.id) == undefined
          ? []
          : RolesDetails.get(element.id);
      roleArray.push(...(arrDtl ? arrDtl : []));
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
}
