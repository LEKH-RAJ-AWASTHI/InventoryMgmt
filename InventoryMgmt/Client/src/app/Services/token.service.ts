import { Injectable } from "@angular/core";
import { ENUM_LocalStorageKeys } from "../shared/shared-enums.service";

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private authTokenKey: string = ENUM_LocalStorageKeys.AuthToken;

  SetToken(token: string) {
    sessionStorage.setItem(this.authTokenKey, token);
  }

  GetToken() {
    const token = sessionStorage.getItem(this.authTokenKey);
    if (!token) {
      console.error('Token is undefined');
    }
    return token;
  }


  ClearToken() {
    return sessionStorage.clear();

  }


}
