import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: any[] = [];

  getCart() {
    return this.cartItems;
  }

  addToCart(game: any) {
    const existingGame = this.cartItems.find(item => item.id === game.id);
    if (existingGame) {
      existingGame.quantity++;
    } else {
      this.cartItems.push({ ...game, quantity: 1 });
    }
  }

  removeFromCart(gameId: number) {
    this.cartItems = this.cartItems.filter(item => item.id !== gameId);
  }

  clearCart() {
    this.cartItems = [];
  }
}
