import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';

export interface AuctionDetails {
  id: string;
  carBrand: string;
  carModel: string;
  imageUrl: string;
  startingPrice: number;
  currentHighestBid: number;
  startTime: string;
  endTime: string;
}

export interface AuctionList {
  id: string;
  carBrand: string;
  carModel: string;
  imageUrl: string;
  currentHighestBid: number;
  endTime: string;
}

export interface CreateAuctionFormData {
  brand: string;
  model: string;
  year: number;
  image: File;
  startingPrice: number;
  startTime: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuctionService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5044/api';

  getAuctionDetails(id: string) {
    return this.http.get<AuctionDetails>(`${this.apiUrl}/Auctions/${id}`);
  }

  placeBid(auctionId: string, amount: number) {
    return this.http.post(`${this.apiUrl}/Bids`, {
      auctionId: auctionId,
      amount: amount
    });
  }

  getAllAuctions() {
    return this.http.get<AuctionList[]>(`${this.apiUrl}/Auctions`, {});
  }

  publishAuction(formData: CreateAuctionFormData) {
    const carData = new FormData();
    carData.append('Brand', formData.brand);
    carData.append('Model', formData.model);
    carData.append('Year', formData.year.toString());
    carData.append('Price', formData.startingPrice.toString());
    carData.append('Image', formData.image);
    return this.http.post<{ carId: string }>(`${this.apiUrl}/Cars`, carData).pipe(
      switchMap(carResponse => {
        return this.http.post(`${this.apiUrl}/Auctions`, {
          carId: carResponse.carId,
          startingPrice: formData.startingPrice,
          startTime: formData.startTime
        });
      })
    );
  }

  getWonAuctions(){
    return this.http.get<AuctionList[]>(`${this.apiUrl}/Auctions/won`)
  }

  getUserAuctions(){
    return this.http.get<AuctionList[]>(`${this.apiUrl}/Auctions/my-listings`)
  }
}
