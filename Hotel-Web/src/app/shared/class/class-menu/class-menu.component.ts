import { r3JitTypeSourceSpan } from '@angular/compiler';
import { Component } from '@angular/core';
import { AppRoles } from 'src/app/public/account';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-class-menu',
  templateUrl: './class-menu.component.html',
  styleUrls: ['./class-menu.component.css']
})
export class ClassMenuComponent {
  currentRoles : AppRoles[] = [];
  isClassManageShow : boolean = false;
  constructor( private sessionStore: SessionStore) {
    this.currentRoles = this.sessionStore.GetRole();
    this.isClassManageShow = this.currentRoles.some(x=>x == AppRoles.Admin || x== AppRoles.Instructor);
      console.log("roles",this.sessionStore.GetRole())
  }

}
