import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassMenuComponent } from './class-menu.component';

describe('ClassMenuComponent', () => {
  let component: ClassMenuComponent;
  let fixture: ComponentFixture<ClassMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
