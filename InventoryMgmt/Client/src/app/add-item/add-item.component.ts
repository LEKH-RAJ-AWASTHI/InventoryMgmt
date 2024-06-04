import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WebService } from '../Services/web.service';


@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {
  AddItemForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private _webService: WebService,
  ) { }

  ngOnInit() {
    this.AddItemForm = this.formBuilder.group({
      itemName: ['', Validators.required],
      brandName: ['', Validators.required],
      unitOfMeasurement: ['', Validators.required],
      purchaseRate: [0, [Validators.required, Validators.min(0)]],
      salesRate: [0, [Validators.required, Validators.min(0)]],
      quantity: [0, [Validators.required, Validators.min(0)]],
      storeName: ['', Validators.required],
      expiryDate: [new Date().toISOString(), Validators.required]
    });
  }

  AddItem() {
    if (this.AddItemForm.valid) {
      const newItem = this.AddItemForm.value;

      this._webService.AddNewItem(newItem).subscribe({
        next: (response: any) => {
          console.log('Item added successfully:', response);
          this.AddItemForm.reset();
        },
        error: (error) => {
          console.error('Error adding item:', error);
        }
      });
    } else {
      console.log('Form is invalid. Please check the fields.');
    }
  }
}
