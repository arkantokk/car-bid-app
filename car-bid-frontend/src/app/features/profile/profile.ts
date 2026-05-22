import {Component, inject, OnInit, signal} from '@angular/core';
import {AuctionList, AuctionService} from '../../core/services/auctionService';
import {AuthService} from '../../core/services/auth';
import {CommonModule} from '@angular/common';
import {RouterLink} from '@angular/router';

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

  ngOnInit(): void {
    this.auctionService.getWonAuctions().subscribe({
      next: (data) => {
        this.wonAuctions.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error fetching won auctions', err);
        this.isLoading.set(false);
      }
    });
  }

}
