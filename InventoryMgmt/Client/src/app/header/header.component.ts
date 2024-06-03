import { Component, EventEmitter, HostListener, Output } from '@angular/core';
import { TokenService } from '../Services/token.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from '../Services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Output() SideNavToggled = new EventEmitter<boolean>();
  MenuStatus: boolean = false;
  IsLoggedIn: boolean = false;
  Subscriptions = new Subscription();
  UserName: string = "";

  constructor(
    private _tokenService: TokenService,
    private _router: Router,
    private _userService: UserService,
  ) {
    // Call the method to get the logged-in status when the component is initialized
    this.GetLoggedInStatus();
  }

  // Host listener for keydown event
  @HostListener('document:keydown', ['$event'])
  PressCtrlPlusSKeyEvent(event: KeyboardEvent) {
    if (event.keyCode == 83 && event.ctrlKey) {
      event.preventDefault();
      if (document.getElementById("id_search_box")) {
        const elem = document.getElementById("id_search_box");
        elem?.focus();
      }
    }
  }

  // Host listener for keydown event
  @HostListener('document:keydown', ['$event'])
  PressM1Event(event: KeyboardEvent) {
    if (event.keyCode == 35 && event.ctrlKey) {
      event.preventDefault();
      if (document.getElementById("menu-1")) {
        const elem = document.getElementById("menu-1");
        elem?.focus();
      }
    }
  }

  SideNavToggle() {
    this.MenuStatus = !this.MenuStatus;
    this.SideNavToggled.emit(this.MenuStatus);
  }

  // Method to get the logged-in status
  GetLoggedInStatus(): void {
    this.Subscriptions.add(this._userService.GetUserLoggedInStatusAsObservable().subscribe(res => {
      if (res) {
        this.IsLoggedIn = res.IsLoggedIn;
        this.UserName = res.UserName
      } else {
        this.IsLoggedIn = false;
        this.UserName = "";
      }
    }));
  }

  // Method to handle logout
  Logout() {
    // Clear the session and navigate to the login page
    let isConfirm = window.confirm("Are you sure you want to logout?");
    if (isConfirm) {
      this.IsLoggedIn = false;
      this._tokenService.ClearToken();
      this._router.navigate(["/login"]);
    }
  }

}
