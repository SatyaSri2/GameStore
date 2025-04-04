// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { CartService } from '../../services/cart.service';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-home',
//   standalone: true,
//   templateUrl: './home.component.html',
//   styleUrls: ['./home.component.css'],
//   imports: [CommonModule]
// })
//   export class HomeComponent {
//     games = [
//       { name: 'Temple Run 2', price: 200, image: 'TempleRun.jpeg', rating: 4.5 },
//       { name: 'Free Fire', price: 399, image: 'Freefire.jpg', rating: 4.7 },
//       { name: 'PUBG Mobile', price: 799, image: 'Pubg.jpg', rating: 4.6 },
//       { name: 'Subway Surfers', price: 200, image: 'Subway.png', rating: 4.4 },
//       { name: 'Call of Duty Mobile', price: 1199, image: 'cod.jpeg', rating: 4.8 },
//       { name: 'Clash of Clans', price: 0, image: 'clash.jpeg', rating: 4.5 },
//       { name: 'Genshin Impact', price: 1599, image: 'genshin.jpeg', rating: 4.6 },
//       { name: 'Asphalt 9', price: 479, image: 'asphalt.jpeg', rating: 4.3 },
//       { name: 'Candy Crush Saga', price: 720, image: 'candy.jpeg', rating: 4.2 },
//       { name: 'Among Us', price: 319, image: 'among.jpeg', rating: 4.7 },
//       { name: 'Fortnite', price: 400, image: 'fort.jpeg', rating: 4.6 },
//       { name: 'Minecraft', price: 2399, image: 'mine.jpeg', rating: 4.8 },
//       { name: 'Roblox', price: 600, image: 'robo.jpeg', rating: 4.5 },
//       { name: 'Clash Royale', price: 1000, image: 'royale.jpeg', rating: 4.4 },
//       { name: 'FIFA Mobile', price: 559, image: 'fifa.jpeg', rating: 4.3 },
//     ];    
  
//     constructor(private router: Router, private cartService: CartService) {}
  
//     navigateToCart() {
//       this.router.navigate(['/cart']);
//     }
  
//     addToCart(game: any) {
//       console.log(`Added ${game.name} to cart!`);
//     }
//   }
  
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  showLoginForm = true; // Toggle between login/signup
  isAuthenticated = false;

  // Form models
  loginData = { email: '', password: '' };
  signupData = { name: '', email: '', password: '', confirmPassword: '' };

  constructor(private router: Router) {}

  toggleForm() {
    this.showLoginForm = !this.showLoginForm;
  }

  onLogin() {
    // Add your actual authentication logic here
    console.log('Login data:', this.loginData);
    this.isAuthenticated = true;
    this.navigateToMenu();
  }

  onSignup() {
    // Add your actual signup logic here
    console.log('Signup data:', this.signupData);
    this.isAuthenticated = true;
    this.navigateToMenu();
  }

  private navigateToMenu() {
    this.router.navigate(['/game-menu']);
  }
}
