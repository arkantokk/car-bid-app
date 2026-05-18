import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuctionService } from '../../../core/services/auctionService';
import { ToastService } from '../../../core/services/toast';

@Component({
  selector: 'app-create-auction',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './create-auction.html'
})
export class CreateAuctionComponent {
  private fb = inject(FormBuilder);
  private auctionService = inject(AuctionService);
  private router = inject(Router);
  private toast = inject(ToastService);

  auctionForm = this.fb.group({
    brand: ['', [Validators.required]],
    model: ['', [Validators.required]],
    startingPrice: [0, [Validators.required, Validators.min(1)]],
    startTime: ['', [Validators.required]]
  });

  onSubmit(): void {
    if (this.auctionForm.invalid) return;

    const rawValues = this.auctionForm.value;
    const formattedData = {
      brand: rawValues.brand!,
      model: rawValues.model!,
      startingPrice: Number(rawValues.startingPrice),
      startTime: new Date(rawValues.startTime!).toISOString()
    };

    this.auctionService.publishAuction(formattedData).subscribe({
      next: () => {
        this.toast.showToast('Auction successfully published!', 'success');
        this.router.navigateByUrl('/home');
      },
      error: (err) => {
        console.error(err);
        this.toast.showToast('Failed to create auction', 'error');
      }
    });
  }
}
