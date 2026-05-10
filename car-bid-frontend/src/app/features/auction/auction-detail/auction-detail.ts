import { Component, OnInit, OnDestroy, inject, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DatePipe, CurrencyPipe, SlicePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuctionService } from '../../../core/services/auction';
import { SignalrService } from '../../../core/services/signalr';

@Component({
  selector: 'app-auction-detail',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, SlicePipe, ReactiveFormsModule],
  templateUrl: './auction-detail.html',
  styleUrls: ['./auction-detail.css']
})
export class AuctionDetailComponent implements OnInit, OnDestroy {
  private route = inject(ActivatedRoute);
  private auctionService = inject(AuctionService);
  private signalRService = inject(SignalrService);
  private cdr = inject(ChangeDetectorRef);
  private fb = inject(FormBuilder);

  auctionId: string | null = null;
  auction: any = null;

  bidForm = this.fb.group({
    amount: [0, [Validators.required, Validators.min(1)]]
  });

  ngOnInit(): void {
    this.auctionId = this.route.snapshot.paramMap.get('id');

    if (this.auctionId) {
      this.loadAuction();
      this.signalRService.startConnection();
      setTimeout(() => {
        this.signalRService.joinAuctionGroup(this.auctionId!);
      }, 1000);
      this.signalRService.newBidReceived$.subscribe((newBid) => {
        if (this.auction) {
          this.auction.bids.unshift(newBid);
          this.auction.currentHighestBid = newBid.amount;
          this.cdr.detectChanges();
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.auctionId) {
      this.signalRService.leaveAuctionGroup(this.auctionId);
    }
  }

  loadAuction() {
    this.auctionService.getAuctionDetails(this.auctionId!).subscribe({
      next: (data) => {
        this.auction = data;
        this.bidForm.patchValue({ amount: this.auction.currentHighestBid + 100 });
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Failed to load auction', err)
    });
  }

  submitBid() {
    if (this.bidForm.invalid) return;

    const amount = this.bidForm.value.amount!;

    this.auctionService.placeBid(this.auctionId!, amount).subscribe({
      next: () => {
        console.log('Bid placed successfully!');
        this.bidForm.markAsPristine();
      },
      error: (err) => {
        if (err.status === 409) {
          alert('Someone else just placed a bid! Refreshing the auction data...');
          this.loadAuction(); // Refresh the page data so they have the latest Version
        } else {
          console.error('Failed to place bid', err);
          const errorMsg = err.error?.detail || 'Bid failed. Make sure your bid is higher than the current one.';
          alert(errorMsg);
        }
      }
    });
  }
}
