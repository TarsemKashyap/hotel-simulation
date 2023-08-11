import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectiveReportComponent } from './objective-report.component';

describe('ObjectiveReportComponent', () => {
  let component: ObjectiveReportComponent;
  let fixture: ComponentFixture<ObjectiveReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObjectiveReportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ObjectiveReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
