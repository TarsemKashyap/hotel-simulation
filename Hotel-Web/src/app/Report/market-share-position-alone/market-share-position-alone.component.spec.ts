import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketShareRoomSoldComponent } from './market-share-roomsold.component';

describe('MarketShareRevenueComponent', () => {
  let component: MarketShareRoomSoldComponent;
  let fixture: ComponentFixture<MarketShareRoomSoldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarketShareRoomSoldComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarketShareRoomSoldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
