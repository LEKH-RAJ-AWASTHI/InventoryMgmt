import { Component } from '@angular/core';
import { ItemDTO } from '../DTO/itemlist.dto';
import { WebService } from '../Services/web.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  Items: ItemDTO[] = [];

  constructor(private _webService: WebService) {
    this.fetchItems();

  }
  ngOnInit(): void {

  }
  fetchItems() {
    this._webService.GetAllProduct().subscribe({
      next: (response: any) => {
        const data: ItemDTO[] = response; // Extracting data from the response
        this.Items = data;
      },
      error: (error: any) => {
        console.error('Error fetching items:', error);
      }
    });
  }
}
