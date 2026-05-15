import { Routes } from '@angular/router';
import { AuctionDetailComponent } from './features/auction/auction-detail/auction-detail';
export const routes: Routes = [
  { path: 'auction/:id', component: AuctionDetailComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];
