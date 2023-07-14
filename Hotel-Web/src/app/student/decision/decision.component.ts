import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-decision',
  templateUrl: './decision.component.html',
  styleUrls: ['./decision.component.css']
})
export class DecisionComponent {
  constructor(private route: ActivatedRoute) {
  }
  
  ngOnInit(): void {
    // this.route.queryParams.subscribe((params) => {
    //  console.log(params['role'],"rolena");
    // });
  }
}
