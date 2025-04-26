import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
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
  
  constructor(private router: Router, private authService: AuthService) {}

  toggleForm() {
    this.showLoginForm = !this.showLoginForm;
    
    this.showForgotPassword = false;
  }
  showForgotPasswordForm() {
    this.showLoginForm = false;
    
    this.showForgotPassword = true;
  }

  backToLogin() {
    this.showLoginForm = true;
    
    this.showForgotPassword = false;
  }

  onLogin() {
    // Add your actual authentication logic here
    console.log('Login data:', this.loginData);
    this.authService.setLoginStatus(true);
    this.router.navigate(['/game-menu']);
  }

  onSignup() {
    // Add your actual signup logic here
    console.log('Signup data:', this.signupData);
    this.authService.setLoginStatus(true);
    this.router.navigate(['/game-menu']);
  }
  showForgotPassword = false;
forgotPasswordEmail = '';
  onForgotPasswordClick() {
    this.showForgotPassword = true;
    this.showLoginForm = false;
  }
  onForgotPassword() {
    console.log('Forgot Password email:', this.forgotPasswordEmail);
    this.authService.sendResetLink(this.forgotPasswordEmail).subscribe({
      next: (res) => {
        alert('Reset link sent to your email.');
        
      },
      error: (err) => {
        console.error('Error:', err);
        alert('Failed to send reset link. Please try again.');
      }
    });
  }

  // private navigateToMenu() {
  //   this.router.navigate(['/game-menu']);
  // }
  onLogout() {
    this.isAuthenticated = false;
    this.router.navigate(['/home']);
  }


}
