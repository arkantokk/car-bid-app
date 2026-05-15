import {inject, Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http';

export interface AuctionDetails {
  id: string;
  carBrand: string;
  carModel: string;
  startingPrice: number;
  currentHighestBid: number;
  endTime: string;
}

@Injectable({
  providedIn: 'root',
})

export class AuctionService {
  private http = inject(HttpClient);
  private readonly apiUrl = "http://localhost:5044/api";

  getAuctionDetails(id: string){
    return this.http.get<AuctionDetails>(`${this.apiUrl}/Auctions/${id}`);
  }

  placeBid(auctionId: string, amount: number){
    return this.http.post(`${this.apiUrl}/Bids`, {
      auctionId: auctionId,
      amount: amount
    })
  }

  getAllAuctions(){
    return this.http.get<AuctionDetails[]>(`${this.apiUrl}/Auctions`, {})
  }
}
