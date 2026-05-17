import {Component, inject, OnInit, signal} from '@angular/core';
import {AuctionDetails, AuctionList, AuctionService} from '../../../core/services/auctionService';
import {RouterLink} from '@angular/router';
import {CurrencyPipe, DatePipe} from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, CurrencyPipe, DatePipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class DashboardComponent implements OnInit {
  private auctionService = inject(AuctionService);
  activeAuctions = signal<AuctionList[]>([]);

  ngOnInit() {
    this.auctionService.getAllAuctions().subscribe(auctions => {
      this.activeAuctions.set(auctions);
    })
  }
}
