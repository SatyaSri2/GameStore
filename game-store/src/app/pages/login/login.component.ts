// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

// @Component({
//   selector: 'app-login',
//   standalone: true,
//   imports: [CommonModule, FormsModule],
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.css']
// })
// export class LoginComponent {
//   email: string = '';
//   password: string = '';
//   errorMessage: string = '';

//   constructor(private authService: AuthService, private router: Router) {}

//   login() {
//     this.authService.login(this.email, this.password).subscribe(
//       (response) => {
//         localStorage.setItem('token', response.token);
//         this.router.navigate(['/home']);
//       },
//       (error) => {
//         this.errorMessage = 'Invalid credentials. Try again!';
//       }
//     );
//   }
// }
