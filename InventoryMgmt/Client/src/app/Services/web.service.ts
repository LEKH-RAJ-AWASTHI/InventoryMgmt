import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NewItem } from '../DTO/itemlist.dto';

@Injectable({
  providedIn: 'root'
})
export class WebService {

  private baseUrl = 'https://localhost:7025/api/';

  public jsonOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private _http: HttpClient) { }

  GetAllProduct() {
    return this._http.get<Response>(`${this.baseUrl}Item/GetAllProduct`);
  }

  AddNewItem(newItem: NewItem) {
    return this._http.post<any>(`${this.baseUrl}Item/AddNewItem`, newItem);
  }
}
