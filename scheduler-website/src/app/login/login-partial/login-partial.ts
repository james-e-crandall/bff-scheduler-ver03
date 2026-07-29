import { Component, inject } from '@angular/core';
import { User } from '../User';
import { SignInManager } from '../sign-in-manager';

@Component({
  selector: 'app-login-partial',
  imports: [],
  templateUrl: './login-partial.html',
  styleUrl: './login-partial.css',
})
export class LoginPartial {
  SignInManager = inject(SignInManager);
  public User:User = {
    Identity: { Name: 'test' }
  }
}
