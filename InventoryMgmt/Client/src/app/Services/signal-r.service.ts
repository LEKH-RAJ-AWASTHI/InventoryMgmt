import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';  // Importing the SignalR library

@Injectable({
  providedIn: 'root'  // This makes the service available throughout the application
})
export class SignalRService {

  private hubConnection!: signalR.HubConnection;  // Declaring a variable for the SignalR connection

  constructor() { }

  public startConnection() {
    // Building the connection to the SignalR hub with automatic reconnection
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7025/hub')  // URL to the SignalR hub endpoint
      .withAutomaticReconnect()  // Enables automatic reconnection in case of disconnection
      .build();  // Completes the building of the HubConnection instance

    // Starting the SignalR connection
    this.hubConnection.start()
      .then(() => console.log('Connection started'))  // Log success message on successful connection
      .catch(err => console.log('Error while starting connection: ' + err));  // Log error message if the connection fails
  }

  // Method to add a listener for inventory updates from the server
  public addInventoryUpdateListener(callback: (itemName: string, quantity: number) => void) {
    // Setting up an event listener for 'ReceiveInventoryUpdate' event from the SignalR hub
    this.hubConnection.on('ReceiveInventoryUpdate', (itemName: string, quantity: number) => {
      callback(itemName, quantity);  // Invoke the callback function with the received data
    });
  }
}
