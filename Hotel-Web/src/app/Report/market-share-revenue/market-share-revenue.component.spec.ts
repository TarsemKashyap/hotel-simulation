import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketShareRevenueComponent } from './market-share-revenue.component';

describe('MarketShareRevenueComponent', () => {
  let component: MarketShareRevenueComponent;
  let fixture: ComponentFixture<MarketShareRevenueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarketShareRevenueComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarketShareRevenueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
