import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvgDailyRateComponent } from './avg-daily-rate.component';

describe('AvgDailyRateComponent', () => {
  let component: AvgDailyRateComponent;
  let fixture: ComponentFixture<AvgDailyRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvgDailyRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvgDailyRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
