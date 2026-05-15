import { Routes } from '@angular/router';
import { AuctionDetailComponent } from './features/auction/auction-detail/auction-detail';
import { LoginComponent } from './features/auth/login/login';
import { RegisterComponent } from './features/auth/register/register';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'auction/:id', component: AuctionDetailComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];
