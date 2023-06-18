import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMonthComponent } from './create-month.component';

describe('CreateMonthComponent', () => {
  let component: CreateMonthComponent;
  let fixture: ComponentFixture<CreateMonthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateMonthComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
