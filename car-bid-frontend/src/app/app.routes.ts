import { Routes } from '@angular/router';
import { AuctionDetailComponent } from './features/auction/auction-detail/auction-detail';
import { LoginComponent } from './features/auth/login/login';
import { RegisterComponent } from './features/auth/register/register';
import {DashboardComponent} from './features/home/dashboard/dashboard'
import {authGuard} from './core/guards/auth-guard';
export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'auction/:id', component: AuctionDetailComponent, canActivate:[authGuard]},
  {path: 'home', component: DashboardComponent, canActivate:[authGuard]},
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];
