// import { NgModule } from '@angular/core';
// import { RouterModule, Routes } from '@angular/router';
// import { HomeComponent } from './pages/home/home.component';
// import { CartComponent } from './pages/cart/cart.component';
// import { LoginComponent } from './pages/login/login.component';

// const routes: Routes = [
//   { path: '', component: HomeComponent },
//   { path: 'cart', component: CartComponent },
//   { path: 'login', component: LoginComponent }
// ];

// @NgModule({
//   imports: [RouterModule.forRoot(routes)],
//   exports: [RouterModule]
// })
// export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { GameMenuComponent } from './game-menu/game-menu.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // Default route
  { path: 'home', component: HomeComponent },
  { path: 'game-menu', component: GameMenuComponent },
  { path: '**', redirectTo: 'home' } // Redirect any unknown path to Home
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
