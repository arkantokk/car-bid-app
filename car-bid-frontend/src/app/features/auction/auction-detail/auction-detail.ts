import {Component, inject, OnDestroy, OnInit, signal, DestroyRef} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {ActivatedRoute} from '@angular/router';

import {AuctionDetails, AuctionService} from '../../../core/services/auctionService';
import {SignalrService, Bid} from '../../../core/services/signalr';
import {ToastService} from '../../../core/services/toast';

@Component({
  selector: 'app-auction-detail',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './auction-detail.html',
  styleUrl: './auction-detail.css',
})
export class AuctionDetailComponent implements OnInit, OnDestroy {

  private route = inject(ActivatedRoute);
  private auctionService = inject(AuctionService);
  private formBuilder = inject(FormBuilder);
  private signalrService = inject(SignalrService);
  private destroyRef = inject(DestroyRef);
  private toast = inject(ToastService);
  private currentAuctionId: string | null = null;
  private timerInterval: number | undefined = undefined;
  timeLeft = signal<number>(-1);
  auction = signal<AuctionDetails | null>(null);
  auctionStatus = signal<'pending' | 'active' | 'ended'>('pending')
  progress = signal<number>(100);
  currentMaxDuration = signal<number>(0)
  bidForm = this.formBuilder.group({
    amount: [[Validators.required]],
  });

  ngOnInit(): void {
    // 1. Subscribe to URL changes
    this.route.paramMap.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(params => {
      const idFromUrl = params.get('id');

      if (idFromUrl) {
        // Leave previous auction group if navigating from one auction to another
        if (this.currentAuctionId) {
          this.signalrService.leaveAuctionGroup(this.currentAuctionId);
        }

        this.currentAuctionId = idFromUrl;

        // Connect to new auction
        this.signalrService.startConnection()
          .then(() => {
            this.signalrService.joinAuctionGroup(idFromUrl);
            this.signalrService.listenToBids();
          });

        // Get auction details
        this.auctionService.getAuctionDetails(idFromUrl).subscribe({
          next: (data) => {
            this.auction.set(data);
            this.startTimer(data.startTime, data.endTime);
            this.bidForm.controls.amount.setValidators([
              Validators.required,
              Validators.min(data.currentHighestBid + 1)
            ]);
            this.bidForm.controls.amount.updateValueAndValidity();
            this.currentMaxDuration.set(new Date(data.endTime).getTime() - new Date(data.startTime).getTime());
            console.log('Auction is loaded:', this.auction());
          },
          error: (err) => {
            console.error('Error fetching auction:', err);
          }
        });
      }
    });

    // 2. Listen to real-time bid updates
    this.signalrService.newBid$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (bid: Bid) => {
          console.log('New bid from SignalR:', bid.amount);
          const currentStartTime = this.auction()?.startTime || '';
          this.currentMaxDuration.set(new Date(bid.endTime).getTime() - new Date(bid.timeStamp).getTime())
          this.startTimer(currentStartTime, bid.endTime);
          this.handleNewBid(bid.amount);
        }
      });
  }

  ngOnDestroy(): void {
    if (this.currentAuctionId) {
      this.signalrService.leaveAuctionGroup(this.currentAuctionId);
    }
    this.signalrService.stopConnection();
  }

  handleNewBid(newHighestBid: number) {
    this.auction.update(currentData => {
      if (!currentData) {
        return null;
      }
      return {
        ...currentData,
        currentHighestBid: newHighestBid
      };
    });
  }

  submitBid(): void {
    if (this.bidForm.invalid) {
      return;
    }
    const amount = this.bidForm.value.amount;
    if (this.currentAuctionId && amount) {
      this.auctionService.placeBid(this.currentAuctionId, amount).subscribe({
        next: () => {
          this.bidForm.reset();
          this.toast.showToast("Bid successfully placed", "success");
        }
      });
    }
  }

  startTimer(startTimeStr: string, endTimeStr: string): void {
    clearInterval(this.timerInterval);

    const safeStartTime = startTimeStr.endsWith('Z') ? startTimeStr : startTimeStr + 'Z';
    const safeEndTime = endTimeStr.endsWith('Z') ? endTimeStr : endTimeStr + 'Z';
    const startMs = new Date(safeStartTime).getTime();
    const endMs = new Date(safeEndTime).getTime();

    this.timerInterval = window.setInterval(() => {
      const now = new Date().getTime();
      const timePercentLeft = endMs - now;
      const percentage = (timePercentLeft / this.currentMaxDuration()) * 100;
      if (now < startMs) {
        this.auctionStatus.set('pending')
        const diffMs = startMs - now;
        this.timeLeft.set(Math.floor(diffMs / 1000));
      } else if (now >= startMs && now < endMs) {
        this.auctionStatus.set('active');
        const diffMs = endMs - now;
        this.progress.set(Math.floor(percentage))
        this.timeLeft.set(Math.floor(diffMs / 1000));
      } else {
        this.auctionStatus.set('ended');
        this.timeLeft.set(0);
        clearInterval(this.timerInterval)
      }
    }, 1000);
  }
}
