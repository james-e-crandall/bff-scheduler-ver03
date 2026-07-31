import { Component, inject } from '@angular/core';
import { User } from '../User';
import { SignInManager } from '../sign-in-manager';
import { UserManager } from '../user-manager';

@Component({
  selector: 'app-login-partial',
  imports: [],
  templateUrl: './login-partial.html',
  styleUrl: './login-partial.css',
})
export class LoginPartial {
  SignInManager = inject(SignInManager);
  UserManager = inject(UserManager);

  public user = this.UserManager.getUserResource();
}
