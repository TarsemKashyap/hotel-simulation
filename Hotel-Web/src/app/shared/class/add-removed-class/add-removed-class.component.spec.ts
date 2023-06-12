import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRemovedClassComponent } from './add-removed-class.component';

describe('AddRemovedClassComponent', () => {
  let component: AddRemovedClassComponent;
  let fixture: ComponentFixture<AddRemovedClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRemovedClassComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRemovedClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
