import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './common/auth-guard.service';
import { InventoryManagementActionsComponent } from './inventory-management-actions/inventory-management-actions.component';
import { SettingComponent } from './setting/setting.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService] },
  {
    path: 'inventory', component: InventoryManagementActionsComponent
    , canActivate: [AuthGuardService]
  },
  { path: 'setting', component: SettingComponent, canActivate: [AuthGuardService] },


  { path: '', redirectTo: 'login', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: 'login' } // Route for undefined routes
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
