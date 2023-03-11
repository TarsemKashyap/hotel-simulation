import { Injectable } from '@angular/core';

const RefreshToken = "RefreshToken";
const AccessToken = "AccessToken";

@Injectable({ providedIn: 'root' })
export class SessionStore {

    constructor() { }

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
      return  localStorage.getItem(AccessToken);
    }

}