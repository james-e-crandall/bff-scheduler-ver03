import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginPartial } from './login-partial';

describe('LoginPartial', () => {
  let component: LoginPartial;
  let fixture: ComponentFixture<LoginPartial>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginPartial],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginPartial);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
