import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart: any[] = [];

  addToCart(game: any) {
    this.cart.push(game);
    console.log('Cart:', this.cart); // Debugging line
  }

  getCart() {
    return this.cart;
  }
}
