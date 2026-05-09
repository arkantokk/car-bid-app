import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection | null = null;
  public newBidReceived$ = new Subject<any>();

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5044/auctionHub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('✅ SignalR Connection Started'))
      .catch(err => console.error('❌ Error while starting SignalR connection: ' + err));

    this.hubConnection.on('ReceiveNewBid', (bidData) => {
      console.log('New bid received via SignalR!', bidData);
      this.newBidReceived$.next(bidData);
    });
  }

  public joinAuctionGroup(auctionId: string) {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('JoinAuctionGroup', auctionId)
        .catch(err => console.error(err));
    }
  }

  public leaveAuctionGroup(auctionId: string) {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('LeaveAuctionGroup', auctionId)
        .catch(err => console.error(err));
    }
  }
}
