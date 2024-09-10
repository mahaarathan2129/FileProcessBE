import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonValidatorComponent } from './common-validator.component';

describe('CommonValidatorComponent', () => {
  let component: CommonValidatorComponent;
  let fixture: ComponentFixture<CommonValidatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommonValidatorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommonValidatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
