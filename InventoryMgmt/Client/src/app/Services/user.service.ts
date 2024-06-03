import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { TokenService } from './token.service';
import { ENUM_LocalStorageKeys } from '../shared/shared-enums.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  User: any;
  UserName: string = "";
  LoggedInSubject = new Subject<any>();

  public jsonOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(
    private _http: HttpClient,
    private _tokenService: TokenService
  ) { }
  Login(credentials: {}): Observable<any> {
    const url = `https://localhost:7025/api/Security/Login`;
    return this._http.post(url, credentials, { responseType: 'text' }).pipe(
      catchError(error => {
        console.error('Error occurred during login:', error);
        return throwError('Login failed. Please try again later.');
      })
    );
  }
  SetUserName(token: string) {
    if (token) {
      this.User = JSON.parse(atob(token.split('.')[1]));

      if (this.User && this.User.UserName) {
        localStorage.setItem(ENUM_LocalStorageKeys.UserName, this.User.UserName);
      }
    }
  }


  GetUserLoggedInStatusAsObservable(): Observable<any> {
    return this.LoggedInSubject.asObservable();
  }

}
