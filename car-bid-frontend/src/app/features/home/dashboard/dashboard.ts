import {Component, inject, OnInit, signal} from '@angular/core';
import {AuctionDetails, AuctionService} from '../../../core/services/auctionService';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class DashboardComponent implements OnInit {
  private auctionService = inject(AuctionService);
  activeAuctions = signal<AuctionDetails[]>([]);

  ngOnInit() {
    this.auctionService.getAllAuctions().subscribe(auctions => {
      this.activeAuctions.set(auctions);
    })
  }
}
