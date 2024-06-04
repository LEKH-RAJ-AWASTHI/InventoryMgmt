import { Component } from '@angular/core';
import { ItemDTO } from '../DTO/itemlist.dto';
import { WebService } from '../Services/web.service';
import { SaleService } from '../Services/sale.service';

@Component({
  selector: 'app-inventory-management-actions',
  templateUrl: './inventory-management-actions.component.html',
  styleUrls: ['./inventory-management-actions.component.css']
})
export class InventoryManagementActionsComponent {
  Items: ItemDTO[] = [];

  constructor(private _webService: WebService, private _saleService: SaleService) {
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

  OpenSaleModal(itemId: number, storeId: number) {
    this._saleService.SetSaleData(storeId, itemId);
  }

}
