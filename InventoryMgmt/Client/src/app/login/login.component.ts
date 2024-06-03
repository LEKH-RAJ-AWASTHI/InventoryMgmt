import { Component } from '@angular/core';
import { UserService } from '../Services/user.service';
import { TokenService } from '../Services/token.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ENUM_LocalStorageKeys } from '../shared/shared-enums.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  UserName = '';
  Password = '';
  Show = false;
  PasswordType = "password";

  ngOnInit() { }

  TogglePasswordVisiblity(event: Event) {
    event.preventDefault();
    if (this.PasswordType === 'password') {
      this.PasswordType = 'text';
      this.Show = true;
    } else {
      this.PasswordType = 'password';
      this.Show = false;
    }
  }

  IsLogInClicked: boolean = false;
  Loading: boolean = true;

  constructor(
    private _userService: UserService,
    private _tokenService: TokenService,
    private _router: Router,
    private _messageBox: ToastrService
  ) {
    if (_tokenService.GetToken()) {
      this._router.navigate(["/dashboard"]);
    }
  }

  OnSubmit() {
    this.IsLogInClicked = true;
    this.Loading = false;
    const credentials = { userName: this.UserName, password: this.Password };

    this._userService.Login(credentials).subscribe({
      next: (response: any) => {
        console.log('Login Response:', response); // Log server response
        this.IsLogInClicked = false;
        if (response) {
          this._tokenService.SetToken(response);
          // console.log('Token set:', this._tokenService.GetToken());
          // this._userService.SetUserName(response.token);
          const userLoggedInStatus = { IsLoggedIn: true, UserName: localStorage.getItem(ENUM_LocalStorageKeys.UserName) || '' };
          this._userService.LoggedInSubject.next(userLoggedInStatus);
          this._router.navigate(['/dashboard']);
        } else {
          this._messageBox.error('Invalid username or password.');
          this.Loading = true;
        }
      },
      error: (error) => {
        this.IsLogInClicked = false;
        console.error('Error occurred during login:', error); // Log login error
        this._messageBox.error('Login failed. Please try again.');
        this.Loading = true;
      }
    });
  }
}