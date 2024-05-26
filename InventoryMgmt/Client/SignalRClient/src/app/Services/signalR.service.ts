import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;

  constructor() { }

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7025/hub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addInventoryUpdateListener(callback: (itemName: string, quantity: number) => void) {
    this.hubConnection.on('ReceiveInventoryUpdate', (itemName: string, quantity: number) => {
      callback(itemName, quantity);
    });
  }
}
