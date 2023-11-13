import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(readonly auth: AuthenticationService) { }

  menuItems = [
    {
      label: 'Polls',
      routerLink: ['polls']
    },
    {
      label: 'New Polls',
      routerLink: ['pollBuilder']
    },
  ]

}
