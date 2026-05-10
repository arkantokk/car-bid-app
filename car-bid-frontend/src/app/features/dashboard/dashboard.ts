import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuctionService } from '../../core/services/auction';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})
export class DashboardComponent {
  private fb = inject(FormBuilder);
  private auctionService = inject(AuctionService);
  private router = inject(Router);

  createForm = this.fb.group({
    brand: ['BMW', Validators.required],
    model: ['M3', Validators.required],
    year: [2024, Validators.required],
    price: [50000, Validators.required],
    startingPrice: [50000, Validators.required],
    daysUntilEnd: [7, Validators.required]
  });

  onSubmit() {
    if (this.createForm.invalid) return;

    const formValues = this.createForm.value;

    this.auctionService.createCar({
      brand: formValues.brand!,
      model: formValues.model!,
      year: formValues.year!,
      price: formValues.price!
    }).subscribe({
      next: (carResponse) => {

        const endDate = new Date();
        endDate.setDate(endDate.getDate() + formValues.daysUntilEnd!);

        this.auctionService.createAuction({
          carId: carResponse.carId,
          startingPrice: formValues.startingPrice!,
          endTime: endDate.toISOString()
        }).subscribe({
          next: (auctionResponse) => {
            alert('Auction Created Successfully!');
            this.router.navigate(['/auction', auctionResponse.auctionId]);
          },
          error: (err) => console.error('Failed to create auction', err)
        });

      },
      error: (err) => console.error('Failed to create car', err)
    });
  }
}
