import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformanceInstComponent } from './performance-inst.component';

describe('PerformanceComponent', () => {
  let component: PerformanceInstComponent;
  let fixture: ComponentFixture<PerformanceInstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PerformanceInstComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PerformanceInstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
