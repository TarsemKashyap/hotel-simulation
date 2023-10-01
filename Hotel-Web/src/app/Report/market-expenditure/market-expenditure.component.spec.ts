import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketExpenditureComponent } from './market-expenditure.component';

describe('MarketExpenditureComponent', () => {
  let component: MarketExpenditureComponent;
  let fixture: ComponentFixture<MarketExpenditureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarketExpenditureComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarketExpenditureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
