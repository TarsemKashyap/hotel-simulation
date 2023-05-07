import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentInitiatedPageComponent } from './payment-initiated-page.component';

describe('PaypalInitiatedPageComponent', () => {
  let component: PaymentInitiatedPageComponent;
  let fixture: ComponentFixture<PaymentInitiatedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentInitiatedPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentInitiatedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
