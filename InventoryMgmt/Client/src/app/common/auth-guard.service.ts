import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { TokenService } from "../Services/token.service";


@Injectable({
  providedIn: "root",
})
export class AuthGuardService implements CanActivate {
  constructor(private _router: Router, private _tokenService: TokenService) { }

  canActivate() {
    // Use your UserService to check if the user is authenticated
    const token = this._tokenService.GetToken();
    if (token) {
      return true; // User is authenticated, allow access
    }
    // User is not authenticated, redirect to the login page or another route
    this._router.navigate(["/login"]);
    return false;
  }
}
