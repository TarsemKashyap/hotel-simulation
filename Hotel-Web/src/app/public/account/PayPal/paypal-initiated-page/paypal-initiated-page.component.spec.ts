import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaypalInitiatedPageComponent } from './paypal-initiated-page.component';

describe('PaypalInitiatedPageComponent', () => {
  let component: PaypalInitiatedPageComponent;
  let fixture: ComponentFixture<PaypalInitiatedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaypalInitiatedPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaypalInitiatedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
