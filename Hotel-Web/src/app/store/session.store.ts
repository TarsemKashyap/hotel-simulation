import { Injectable } from '@angular/core';
import { AppRoles } from '../public/account';

const RefreshToken = 'RefreshToken';
const AccessToken = 'AccessToken';
const userRole = 'userRole';

@Injectable({ providedIn: 'root' })
export class SessionStore {
  constructor() {}

  SetRefreshToken(value: string) {
    localStorage.setItem(RefreshToken, value);
  }

  GetRefreshToken() {
    return localStorage.getItem(RefreshToken);
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
