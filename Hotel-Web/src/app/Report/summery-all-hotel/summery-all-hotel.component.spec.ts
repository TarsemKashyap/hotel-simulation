import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummeryAllHotelComponent } from './summery-all-hotel.component';

describe('SummeryAllHotelComponent', () => {
  let component: SummeryAllHotelComponent;
  let fixture: ComponentFixture<SummeryAllHotelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SummeryAllHotelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SummeryAllHotelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
