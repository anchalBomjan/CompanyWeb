import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignJobComponent } from './assign-job.component';

describe('AssignJobComponent', () => {
  let component: AssignJobComponent;
  let fixture: ComponentFixture<AssignJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignJobComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssignJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
