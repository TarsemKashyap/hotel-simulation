import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionStore } from 'src/app/store';

@Component({
  selector: 'app-decision',
  templateUrl: './decision.component.html',
  styleUrls: ['./decision.component.css']
})
export class DecisionComponent {
  currentRoleName : string;
  constructor(private route: ActivatedRoute , private sessionStore: SessionStore) {
    this.currentRoleName = this.sessionStore.GetCurrentRole() || '';
  }
  
  ngOnInit(): void {
    
  }
}
