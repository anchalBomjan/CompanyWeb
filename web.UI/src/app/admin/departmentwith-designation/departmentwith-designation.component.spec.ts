import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentwithDesignationComponent } from './departmentwith-designation.component';

describe('DepartmentwithDesignationComponent', () => {
  let component: DepartmentwithDesignationComponent;
  let fixture: ComponentFixture<DepartmentwithDesignationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentwithDesignationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartmentwithDesignationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
