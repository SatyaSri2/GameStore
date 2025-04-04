// import { Routes } from '@angular/router';
// import { HomeComponent } from './pages/home/home.component';
// import { CartComponent } from './pages/cart/cart.component';
// import { LoginComponent } from './pages/login/login.component';

// export const routes: Routes = [
//   { path: '', component: HomeComponent },
//   { path: 'cart', component: CartComponent },
//   { path: 'login', component: LoginComponent }
// ];

// import { Routes } from '@angular/router';

// export const routes: Routes = [];

import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { GameMenuComponent } from './game-menu/game-menu.component';

export const routes: Routes = [
  {
    path: '', component: HomeComponent ,
 },
  { 
    path: 'game-menu', 
    component: GameMenuComponent // This will render in the router-outlet
  },
  
  // Add other routes as needed
];