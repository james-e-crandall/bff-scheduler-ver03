import { Routes } from '@angular/router';
import { HomePage } from './home/home-page/home-page';

const homePageRoute = {
  path: '',
  component: HomePage,
};

const notFoundPageRoute = {
  path: '**',
  component: HomePage,
};

export const routes: Routes = [
  homePageRoute,
  notFoundPageRoute
];
