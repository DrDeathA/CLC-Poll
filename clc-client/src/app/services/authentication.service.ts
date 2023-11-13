import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../interfaces/user';
import { UserAuthDetails } from '../interfaces/user-auth-details';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { RegisterUser } from '../interfaces/register-user';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl = environment.API + 'user/';
  jwtHelper = new JwtHelperService();
  public user: User | null = null;
  public token: string | null = null;

  constructor(readonly http: HttpClient, readonly router: Router, readonly toastr: ToastrService) { }

  login(user: UserAuthDetails) {
    this.http.post(this.baseUrl + 'login', user, { responseType: 'text' }).subscribe(
     response => {
      const token = response;
      if (token) {
        localStorage.setItem('pollToken', token);
        this.setCurrentUser();
      }
     })
  }

  register(user: RegisterUser) {
    this.http.post(this.baseUrl + 'register', user, { responseType: 'text' }).subscribe(
     response => {
      this.toastr.success(response);
      console.log(response);
    })
  }

  setCurrentUser() {
    const storageToken = localStorage.getItem('pollToken');
    if(!storageToken) return;
    this.token = storageToken;
    this.user = this.jwtHelper.decodeToken(storageToken);
    this.router.navigateByUrl('polls');
  }
  
  logout() {
    localStorage.removeItem('pollToken');
    this.token = null;
    this.user = null;
    this.router.navigateByUrl('login');
  }
}
