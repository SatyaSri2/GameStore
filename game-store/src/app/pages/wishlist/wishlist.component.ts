import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WishlistService } from '../../services/wishlist.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent {
  wishlistItems: any[] = [];

  constructor(private wishlistService: WishlistService, private router: Router) {}

  ngOnInit() {
    this.wishlistItems = this.wishlistService.getWishlist();
  }

  removeItem(gameId: number) {
    this.wishlistService.removeFromWishlist(gameId);
    this.wishlistItems = this.wishlistService.getWishlist();
  }

  clearWishlist() {
    this.wishlistService.clearWishlist();
    this.wishlistItems = [];
  }

  goToMenu() {
    this.router.navigate(['/game-menu']);
  }
}
