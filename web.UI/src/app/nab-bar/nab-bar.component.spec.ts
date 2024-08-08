import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NabBarComponent } from './nab-bar.component';

describe('NabBarComponent', () => {
  let component: NabBarComponent;
  let fixture: ComponentFixture<NabBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NabBarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NabBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
