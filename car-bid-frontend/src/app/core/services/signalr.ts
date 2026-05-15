import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

export interface Bid {
  auctionId: string;
  bidOwner: string;
  amount: number;
  timeStamp: string;
}

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection: signalR.HubConnection | null = null;
  public newBid$ = new Subject<Bid>();

  private readonly hubUrl = "http://localhost:5044/auctionHub";

  async startConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .withAutomaticReconnect()
      .build();

    try {
      await this.hubConnection.start();
      return console.log('SignalR Connected!');
    } catch (err) {
      return console.error('SignalR Connection Error: ', err);
    }
  }

  stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => console.log('SignalR Disconnected.'));
    }
  }

  listenToBids() {
    if (this.hubConnection) {
      this.hubConnection.on("ReceiveNewBid", (data: Bid) => {
        this.newBid$.next(data);
      });
    }
  }

  joinAuctionGroup(auctionId: string) {
    if (this.hubConnection) {
      this.hubConnection.invoke("JoinAuctionGroup", auctionId)
        .then(() => console.log(`Joined auction group ${auctionId}`))
        .catch(err => console.error('Error joining group:', err));
    }
  }

  leaveAuctionGroup(auctionId: string) {
    if (this.hubConnection) {
      this.hubConnection.invoke("LeaveAuctionGroup", auctionId)
        .then(() => console.log(`Left auction group ${auctionId}`))
        .catch(err => console.error('Error leaving group:', err));
    }
  }
}
