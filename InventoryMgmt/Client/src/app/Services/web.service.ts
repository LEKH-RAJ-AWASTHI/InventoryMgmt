import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NewItem } from '../DTO/add-new-item.dto';
import { SaleData } from '../DTO/sales.dto';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class WebService {

  private baseUrl = 'https://localhost:7025/api/';

  constructor(private _http: HttpClient, private _tokenService: TokenService) { }

  getAuthHeaders(): HttpHeaders {
    const token = this._tokenService.GetToken();
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  GetAllProduct() {
    return this._http.get<Response>(`${this.baseUrl}Item/GetAllProduct`, { headers: this.getAuthHeaders() });
  }

  AddNewItem(newItem: NewItem) {
    return this._http.post<any>(`${this.baseUrl}Item/AddNewItem`, newItem, { headers: this.getAuthHeaders() });
  }

  SaleItems(SaleItem: SaleData) {
    return this._http.post<any>(`${this.baseUrl}Sales`, SaleItem, { headers: this.getAuthHeaders() });
  }
}
