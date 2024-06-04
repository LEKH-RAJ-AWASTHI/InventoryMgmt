import { Component } from '@angular/core';
import { SaleData } from '../DTO/sales.dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WebService } from '../Services/web.service';

@Component({
  selector: 'app-sale-item',
  templateUrl: './sale-item.component.html',
  styleUrls: ['./sale-item.component.css']
})
export class SaleItemComponent {
  SaleForm!: FormGroup;
  saleData: SaleData = new SaleData(); // Declare a property for the SaleData object

  constructor(private _formBuilder: FormBuilder, private _webService: WebService) { }

  ngOnInit() {
    // Initialize the reactive form
    this.SaleForm = this._formBuilder.group({
      storeId: [0, Validators.required],
      itemId: [0, Validators.required],
      quantity: [0, Validators.required]
    });

  }

  SaleItem() {
    if (this.SaleForm.valid) {
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


