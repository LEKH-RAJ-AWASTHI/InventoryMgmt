import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../Services/signal-r.service';
import { Notification } from '../DTO/notification.dto';
@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  Notifications: Notification[] = [];  // Use the Notification interface

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    console.log('NotificationComponent initialized');
    this.signalRService.startConnection();
    this.signalRService.addInventoryUpdateListener((data: any) => {
      console.log(data);

      // this.Notifications.push({ itemName: data.item, quantity: data.quantity, timestamp: new Date(), storeName: data.storeName });
      this.Notifications.push(data);

      this.sortNotifications();
    });
  }

  private sortNotifications() {
    this.Notifications.sort((a, b) => b.timestamp.getTime() - a.timestamp.getTime());
  }
}
