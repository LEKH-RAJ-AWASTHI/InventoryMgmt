import { Component } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {

  SideNavList = [
    {
      number: '1',
      name: 'Dashboard',
      icon: 'fas fa-tachometer-alt, fa-solid fa-table-columns'
    },
    {
      number: '1',
      name: 'Inventory',
      icon: 'fa-solid fa-warehouse'
    },
    {
      number: '1',
      name: 'Orders',
      icon: 'fa-brands fa-first-order'
    },
    {
      number: '1',
      name: 'Reports',
      icon: 'fa-solid fa-flag'
    },
    {
      number: '1',
      name: 'Settings',
      icon: 'fa-solid fa-bars'
    },
    {
      number: '1',
      name: 'Help & Support',
      icon: 'fa-solid fa-handshake-angle'
    },
    {
      number: '1',
      name: 'Profile',
      icon: 'fa-solid fa-user'
    }

  ];

}
