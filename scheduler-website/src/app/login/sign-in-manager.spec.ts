import { TestBed } from '@angular/core/testing';

import { SignInManager } from './sign-in-manager';

describe('SignInManager', () => {
  let service: SignInManager;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignInManager);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
