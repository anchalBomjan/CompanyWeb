import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DapartmentwithDesignationComponent } from './dapartmentwith-designation.component';

describe('DapartmentwithDesignationComponent', () => {
  let component: DapartmentwithDesignationComponent;
  let fixture: ComponentFixture<DapartmentwithDesignationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DapartmentwithDesignationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DapartmentwithDesignationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
