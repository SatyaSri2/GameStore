import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule], // Added required imports
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  showProfileModal = false;
  constructor(public authService: AuthService) {}
  user = {
    name: 'Sara',
    email: 'Sara123@gmail.com',
    phone: '9854325698',
    address: 'Main Road,RJY'
  };

  toggleProfileModal() {
    this.showProfileModal = !this.showProfileModal;
    document.body.style.overflow = this.showProfileModal ? 'hidden' : '';
  }
  
}