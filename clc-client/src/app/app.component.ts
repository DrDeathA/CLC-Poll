import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'clc-client';
  
  constructor(readonly auth: AuthenticationService) {}
  
  ngOnInit(): void {
    this.auth.setCurrentUser();
  }
}
