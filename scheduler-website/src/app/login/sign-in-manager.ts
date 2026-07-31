import { Injectable } from '@angular/core';
import { User } from './User';
import { httpResource, HttpResourceRef } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class SignInManager {
  public IsSignedIn(user:User): boolean | undefined{
    return user.isAuthenticated;
  }
}
