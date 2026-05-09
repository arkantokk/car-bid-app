import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CreateCarRequest {
  brand: string;
  model: string;
  year: number;
  price: number;
}

export interface CreateAuctionRequest {
  carId: string;
  startingPrice: number;
  endTime: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5044/api';

  createCar(request: CreateCarRequest): Observable<{ carId: string }> {
    return this.http.post<{ carId: string }>(`${this.apiUrl}/Cars`, request);
  }

  createAuction(request: CreateAuctionRequest): Observable<{ auctionId: string }> {
    return this.http.post<{ auctionId: string }>(`${this.apiUrl}/Auctions`, request);
  }

  getAuctionDetails(auctionId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Auctions/${auctionId}`);
  }

  placeBid(auctionId: string, amount: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/Bids`, { auctionId, amount });
  }
}
