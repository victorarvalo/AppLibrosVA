import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateResenaComponent } from './create.resena.component';

describe('CreateResenaComponent', () => {
  let component: CreateResenaComponent;
  let fixture: ComponentFixture<CreateResenaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateResenaComponent]
    });
    fixture = TestBed.createComponent(CreateResenaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
