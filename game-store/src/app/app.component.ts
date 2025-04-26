// import { Component } from '@angular/core';
// import { RouterModule } from '@angular/router';
// import { NavbarComponent } from './components/navbar/navbar.component';

// @Component({
//   selector: 'app-root',
//   standalone: true,
//   imports: [RouterModule, NavbarComponent],
//   template: `
//     <app-navbar></app-navbar>
//     <router-outlet></router-outlet>
//   `,
// })
// export class AppComponent {}


import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    RouterModule
  ],
  template: `
    <app-navbar *ngIf="showNavbar"></app-navbar>
    <main>
      <router-outlet></router-outlet>
    </main>
    
  `,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  showNavbar = true;
  showFooter = true;

  constructor(private router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        const hideOnPaths = ['/', '/login']; // add any path you want to hide navbar
        this.showNavbar = !hideOnPaths.includes(event.urlAfterRedirects);
      });
    }
  }