import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { ApplicationWrapperComponent } from './application-wrapper/application-wrapper.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuardService } from './common/auth-guard.service';
import { AuthInterceptorService } from './common/auth-interceptor.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotificationComponent } from './notification/notification.component';
import { ItemListComponent } from './item-list/item-list.component';
import { InventoryManagementActionsComponent } from './inventory-management-actions/inventory-management-actions.component';
import { AddItemComponent } from './add-item/add-item.component';
import { SaleItemComponent } from './sale-item/sale-item.component';
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ApplicationWrapperComponent,
    SideNavComponent,
    DashboardComponent,
    LoginComponent,
    NotificationComponent,
    ItemListComponent,
    InventoryManagementActionsComponent,
    AddItemComponent,
    SaleItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right', }),
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    AuthGuardService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
