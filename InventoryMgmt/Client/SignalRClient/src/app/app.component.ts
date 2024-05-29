import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SignalrService } from './Services/signalR.service';
import { HttpClient, HttpContext } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'Inventory Notification App';
  inventoryUpdates: string[] = [];
  constructor(private signalrService: SignalrService) { }
  ngOnInit() {
    this.signalrService.startConnection();
    this.signalrService.addInventoryUpdateListener((itemName, quantity) => {
      const message = `Item: ${itemName}, Quantity: ${quantity} is below threshold.`;
      this.inventoryUpdates.push(message);
    });
    // this.startHttpRequest();
  }
  // private startHttpRequest = () =>{
  //   this.http.get('http://localhost:7025/api/Notification')
  //             .subscribe(res=> {
  //               console.log(res);
  //             });
  // }
}

