import { Component } from '@angular/core';
import { LoginPartial } from '../../login/login-partial/login-partial';

@Component({
  selector: 'app-home-page',
  imports: [LoginPartial],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {}
