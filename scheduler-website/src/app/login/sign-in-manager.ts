import { Injectable } from '@angular/core';
import { User } from './User';

@Injectable({
  providedIn: 'root',
})
export class SignInManager {
  IsSignedIn(user:User):boolean{
    return false;
  }
}
