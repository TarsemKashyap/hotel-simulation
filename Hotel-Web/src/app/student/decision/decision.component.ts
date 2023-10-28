import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IHyperLinks } from 'src/app/Report/report-list/IHyperLinks.model';
import { SessionStore } from 'src/app/store';
@Component({
  selector: 'app-decision',
  templateUrl: './decision.component.html',
  styleUrls: ['./decision.component.css'],
})
export class DecisionComponent {
  currentRoleName: string;
  activeLinks: IHyperLinks[] = [];
  private decisionLinks: IHyperLinks[] = [
    {
      link: '../room',
      text: 'Room Allocation',
      enable: () =>
        this.currentRoleName === 'GM' || this.currentRoleName === 'RO',
    },
    {
      link: '../attribute',
      text: 'Hotel Attributes and Amenities',
      enable: () =>
        this.currentRoleName === 'GM' ||
        this.currentRoleName === 'RO' ||
        this.currentRoleName == 'FB' ||
        this.currentRoleName == 'RT',
    },
    {
      link: '../price',
      text: 'Room Rate Decisions',
      enable: () =>
        this.currentRoleName === 'GM' || this.currentRoleName === 'RM',
    },
    {
      link: '../marketing',
      text: 'Marketing Decisions',
      enable: () =>
        this.currentRoleName === 'GM' || this.currentRoleName === 'MM',
    },
  ];
  constructor(
    private route: ActivatedRoute,
    private sessionStore: SessionStore
  ) {
    this.currentRoleName = this.sessionStore.GetCurrentRole() || '';
  }

  ngOnInit(): void {
    this.activeLinks = this.decisionLinks.filter((x) => {
      if (!x.enable) return true;
      return x.enable();
    });
  }
}
