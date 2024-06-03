import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {
  @Input() SideNavStatus: boolean = false;
  SideNavList = [
    {
      number: '1',
      name: 'Dashboard',
      icon: 'fas fa-tachometer-alt, fa-solid fa-table-columns',
      route: '/dashboard'
    },
    {
      number: '1',
      name: 'Inventory',
      icon: 'fa-solid fa-warehouse',
      route: '/inventory'
    },
    {
      number: '1',
      name: 'Orders',
      icon: 'fa-brands fa-first-order',
      route: ''
    },
    {
      number: '1',
      name: 'Reports',
      icon: 'fa-solid fa-flag',
      route: ''
    },
    {
      number: '1',
      name: 'Settings',
      icon: 'fa-solid fa-bars',
      route: ''
    },
    {
      number: '1',
      name: 'Help & Support',
      icon: 'fa-solid fa-handshake-angle',
      route: ''
    },
    {
      number: '1',
      name: 'Profile',
      icon: 'fa-solid fa-user',
      route: ''
    }

  ];

}
