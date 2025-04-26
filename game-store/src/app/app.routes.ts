import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { GameMenuComponent } from './game-menu/game-menu.component';
import { CartComponent } from './pages/cart/cart.component';
import { WishlistComponent } from './pages/wishlist/wishlist.component';
export const routes: Routes = [
  {
    path: '', 
    redirectTo: 'home', 
    pathMatch: 'full' 
  },
  { 
    path: 'home',
    component: HomeComponent 
  },
  { 
    path: 'game-menu', 
    component: GameMenuComponent 
  },
  { 
    path: 'cart', 
    component: CartComponent 
  },
  {
    path: 'wishlist',
    component: WishlistComponent 
  }
];