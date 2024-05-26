import { r3JitTypeSourceSpan } from '@angular/compiler';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppRoles } from 'src/app/public/account';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-class-menu',
  templateUrl: './class-menu.component.html',
  styleUrls: ['./class-menu.component.css'],
})
export class ClassMenuComponent {
  currentRoles: AppRoles[] = [];
  isClassManageShow: boolean = false;
  classId?: number;
  constructor(private sessionStore: SessionStore,private route:ActivatedRoute) {
    this.currentRoles = this.sessionStore.GetRole();
    this.isClassManageShow = this.currentRoles.some(
      (x) => x == AppRoles.Admin || x == AppRoles.Instructor
    );
    this.classId=this.route.snapshot.params['id'];
  }
}
