// import { bootstrapApplication } from '@angular/platform-browser';
// import { AppComponent } from './app/app.component';
// import { provideRouter } from '@angular/router';
// import { HomeComponent } from './app/pages/home/home.component';
// import { CartComponent } from './app/pages/cart/cart.component';

// bootstrapApplication(AppComponent, {
//   providers: [
//     provideRouter([
//       { path: '', component: HomeComponent }, // ✅ Home page route
//       { path: 'cart', component: CartComponent } // ✅ Cart page route
//     ])
//   ]
// });

import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideHttpClient } from '@angular/common/http'; 

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes), provideHttpClient()]
}).catch(err => console.error(err));
