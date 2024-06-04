import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  storeId: number = 0;
  itemId: number = 0;

  SetSaleData(storeId: number, itemId: number) {
    this.storeId = storeId;
    this.itemId = itemId;
  }

  GetSaleData() {
    return { StoreId: this.storeId, ItemId: this.itemId };

  }
}
