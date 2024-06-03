import { Component, OnInit } from '@angular/core';
import { ItemDTO } from '../DTO/itemlist.dto';
import { WebService } from '../Services/web.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
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
