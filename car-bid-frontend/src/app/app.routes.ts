import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login';
import { DashboardComponent } from './features/dashboard/dashboard';
import {RegisterComponent} from './features/auth/register/register';
import {AuctionDetailComponent} from './features/auction/auction-detail/auction-detail';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'auction/:id', component: AuctionDetailComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];
