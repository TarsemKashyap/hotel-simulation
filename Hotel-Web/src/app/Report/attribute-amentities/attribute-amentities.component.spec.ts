import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttributeAmentitiesComponent } from './attribute-amentities.component';

describe('MarketShareRevenueComponent', () => {
  let component: AttributeAmentitiesComponent;
  let fixture: ComponentFixture<AttributeAmentitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttributeAmentitiesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttributeAmentitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
