import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart.service';  // ✅ Correct path

@Component({
  selector: 'app-cart',
  standalone: true,
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  imports: [CommonModule] 
})
export class CartComponent {
  cartItems: any[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.cartItems = this.cartService.getCart();
    console.log('Cart Items:', this.cartItems); // Debugging line
  }
  removeItem(gameId: number) {
    this.cartService.removeFromCart(gameId);
    this.cartItems = this.cartService.getCart();
  }
  
  clearCart() {
    this.cartService.clearCart();
    this.cartItems = [];
  }
  get totalPrice() {
    return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  }
  checkout() {
    // Implement checkout logic here
    console.log('Proceeding to checkout with items:', this.cartItems);
    // After checkout, clear the cart
    this.clearCart();  
}
}
