import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileProcessComponent } from './file-process.component';

describe('FileProcessComponent', () => {
  let component: FileProcessComponent;
  let fixture: ComponentFixture<FileProcessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileProcessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
