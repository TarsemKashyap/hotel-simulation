import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentDecisionsComponent } from './student-decisions.component';

describe('StudentDecisionsComponent', () => {
  let component: StudentDecisionsComponent;
  let fixture: ComponentFixture<StudentDecisionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentDecisionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentDecisionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
