import { Component } from '@angular/core';
import { TokenService } from '../Services/token.service';

@Component({
  selector: 'app-application-wrapper',
  templateUrl: './application-wrapper.component.html',
  styleUrls: ['./application-wrapper.component.css']
})
export class ApplicationWrapperComponent {
  SideNavStatus: boolean = false;

  constructor(private tokenService: TokenService) { }

  isAuthenticated(): boolean {
    return !!this.tokenService.GetToken();
  }
}
