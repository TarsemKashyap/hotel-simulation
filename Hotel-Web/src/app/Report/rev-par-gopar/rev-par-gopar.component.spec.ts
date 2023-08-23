import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevParGoparComponent } from './rev-par-gopar.component';

describe('RevParGoparComponent', () => {
  let component: RevParGoparComponent;
  let fixture: ComponentFixture<RevParGoparComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevParGoparComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevParGoparComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
