import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentRolesEditComponent } from './student-roles-edit.component';

describe('StudentRolesEditComponent', () => {
  let component: StudentRolesEditComponent;
  let fixture: ComponentFixture<StudentRolesEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentRolesEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentRolesEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
