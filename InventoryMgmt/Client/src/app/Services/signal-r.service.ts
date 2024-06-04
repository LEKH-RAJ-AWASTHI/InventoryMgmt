import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  constructor(private tokenService: TokenService) { }

  public startConnection() {
    const token = this.tokenService.GetToken()

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7025/hub', {
        accessTokenFactory: () => token ?? ''
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.onreconnected(() => console.log('Reconnected'));
    this.hubConnection.onreconnecting(() => console.log('Reconnecting...'));
    this.hubConnection.onclose(() => console.log('Connection closed'));
  }

  public addInventoryUpdateListener(callback: (itemName: string, quantity: number, timestamp: Date, storeName: string) => void) {
    this.hubConnection.on('GetLowStockNotification', (itemName: string, quantity: number, timestamp: string, storeName: string) => {
      console.log('New notification received:', itemName, quantity, timestamp, storeName);
      callback(itemName, quantity, new Date(timestamp), storeName);  // Convert string to Date
    });
  }
}