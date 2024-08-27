import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { AccountService } from './public/account';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Hotel-Web';
  /**
   *
   */
  constructor(private accountService: AccountService, private router: Router) {
    // Chart.register(ChartDataLabels);
  }
  ngOnInit(): void {
    this.accountService.$sessionExpired.subscribe((x) => {
      this.accountService.clearSession();
      this.router.navigate(['login']);
    });
  }
}
