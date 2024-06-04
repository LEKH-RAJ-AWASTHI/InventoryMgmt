import { Component, OnInit } from '@angular/core';
import { SaleData } from '../DTO/sales.dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WebService } from '../Services/web.service';
import { SaleService } from '../Services/sale.service';

@Component({
  selector: 'app-sale-item',
  templateUrl: './sale-item.component.html',
  styleUrls: ['./sale-item.component.css']
})
export class SaleItemComponent implements OnInit {
  SaleForm!: FormGroup;
  saleData: SaleData = new SaleData(); // Declare a property for the SaleData object
  StoreId: number = 0;
  ItemId: number = 0;

  constructor(private _formBuilder: FormBuilder, private _webService: WebService, private _saleService: SaleService) {
    this.SaleForm = this._formBuilder.group({
      'storeId': [0, Validators.required],
      'itemId': [0, Validators.required],
      'quantity': [0, Validators.required]
    });
  }

  ngOnInit() {

  }
  ValidateSalesData() {
    const quantity = this.SaleForm.get('quantity')?.value;
    const saleData = this._saleService.GetSaleData();
    this.StoreId = this._saleService.storeId;
    this.SaleForm.controls["storeId"].setValue(this.StoreId);
    this.ItemId = this._saleService.itemId;
    this.SaleForm.controls["itemId"].setValue(this.ItemId);
  }
  SaleItem() {
    this.ValidateSalesData();
    if (this.SaleForm.valid) {
      const quantity = this.SaleForm.get('quantity')?.value;
      this.SaleForm.patchValue({ quantity: quantity });
      const saleData: SaleData = this.SaleForm.value;
      console.log('Sale Data:', saleData); // Log the data to check format

      this._webService.SaleItems(saleData).subscribe({
        next: (response: any) => {
          console.log('Sale successful:', response);
          this.SaleForm.reset();
        },
        error: (error: any) => {

          console.error('Sale failed:', error);
        }
      });
    } else {
      console.log('Form is invalid. Please check the fields.');
    }
  }
}


