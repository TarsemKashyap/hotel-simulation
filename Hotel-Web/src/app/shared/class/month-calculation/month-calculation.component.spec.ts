import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthCalculationComponent } from './month-calculation.component';

describe('MonthCalculationComponent', () => {
  let component: MonthCalculationComponent;
  let fixture: ComponentFixture<MonthCalculationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthCalculationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthCalculationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
