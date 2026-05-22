import {Component, inject, OnInit, signal} from '@angular/core';
import {AuctionList, AuctionService} from '../../core/services/auctionService';
import {AuthService} from '../../core/services/auth';
import {CommonModule} from '@angular/common';
import {RouterLink} from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, RouterLink],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class ProfileComponent implements OnInit {
  auctionService = inject(AuctionService);
  authService = inject(AuthService);
  wonAuctions= signal<AuctionList[]>([]);
  isLoading = signal<boolean>(true);
  myAuctions = signal<AuctionList[]>([]);
  activeTab = signal<'won' | 'my'>('won');


  ngOnInit(): void {

    this.isLoading.set(true);
    forkJoin({
      auctions: this.auctionService.getUserAuctions(),
      wonAuctions: this.auctionService.getWonAuctions()
    }).subscribe({
      next: (results) => {
        this.wonAuctions.set(results.wonAuctions);
        this.myAuctions.set(results.auctions);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error fetching profile data', err);
        this.isLoading.set(false);
      }
    })
  }

}
