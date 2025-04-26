import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlist: any[] = [];

  getWishlist(): any[] {
    return this.wishlist;
  }

  addToWishlist(game: any) {
    const exists = this.wishlist.find(item => item.id === game.id);
    if (!exists) {
      this.wishlist.push(game);
    }
  }

  removeFromWishlist(gameId: number) {
    this.wishlist = this.wishlist.filter(item => item.id !== gameId);
  }

  clearWishlist() {
    this.wishlist = [];
  }
}
