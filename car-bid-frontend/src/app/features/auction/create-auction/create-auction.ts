import {Component, inject, signal} from '@angular/core';
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
  selectedFile: File | null = null;
  imagePreview = signal<string | null>(null);

  auctionForm = this.fb.group({
    brand: ['', [Validators.required]],
    model: ['', [Validators.required]],
    year: [new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(2026)]],
    startingPrice: [0, [Validators.required, Validators.min(1)]],
    startTime: ['', [Validators.required]]
  });

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview.set(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit(): void {
    if (this.auctionForm.invalid || !this.selectedFile) {
      this.toast.showToast('Please fill all fields and select an image', 'error');
      return;
    }

    const rawValues = this.auctionForm.value;
    const val = this.auctionForm.value;
    const formData = {
      brand: rawValues.brand!,
      model: rawValues.model!,
      year: Number(val.year),
      startingPrice: Number(rawValues.startingPrice),
      startTime: new Date(rawValues.startTime!).toISOString(),
      image: this.selectedFile
    };

    this.auctionService.publishAuction(formData).subscribe({
      next: () => {
        this.toast.showToast('Auction successfully published!', 'success');
        this.router.navigateByUrl('/home');
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
