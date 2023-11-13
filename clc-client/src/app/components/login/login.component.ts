import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from 'src/app/interfaces/register-user';
import { UserAuthDetails } from 'src/app/interfaces/user-auth-details';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  login: UserAuthDetails = {
    email: '',
    password: ''
  };
  register: RegisterUser = {
    name: '',
    surname: '',
    email: '',
    password: ''
  };
  tempPassword: string = '';

  constructor(readonly auth: AuthenticationService, readonly toastr: ToastrService) {

  }

  loginUser() {
    if (!this.login.email || !this.login.password) {
      this.toastr.error("Please Ensure All Required Data is Completed");
      return;
    }

    this.auth.login(this.login);
  }

  registerUser() {
    if (!this.register.name || !this.register.surname ||
      !this.register.email || !this.register.password) {
      this.toastr.error("Please Ensure All Required Data is Completed");
      return;
    }

    if (this.tempPassword === this.register.password) {
      this.auth.register(this.register);
      this.register = {
        name: '',
        surname: '',
        email: '',
        password: ''
      };
    }
    else {
      this.toastr.error("Passwords Didn't Match");
    }
    this.tempPassword = '';
  }
}
