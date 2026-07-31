import { httpResource, HttpResourceRef } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './User';

@Injectable({
  providedIn: 'root',
})
export class UserManager {
  // Returns a reactive HttpResourceRef
  getUserResource(): HttpResourceRef<User | undefined> {
    return httpResource<User>(() => {
      // Returning undefined tells the resource to remain idle until a valid ID is present
      return  `/bff/user`;
    });
  }
}
