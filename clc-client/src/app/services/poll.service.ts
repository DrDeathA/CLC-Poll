import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Poll } from '../interfaces/poll';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class PollService {
  baseUrl = environment.API + 'poll/';
  globalPoll: Poll | null = null;

  constructor(readonly http: HttpClient, readonly router: Router, readonly auth: AuthenticationService,
    readonly toastr: ToastrService) { }

  viewPolls() {
    return this.http.get(this.baseUrl + 'getPolls');
  }

  saveNewPoll(poll: Poll) {
    this.http.post(this.baseUrl + `create/${this.auth.user?.nameid}`, poll, { responseType: 'text' }).subscribe(x => {
      this.toastr.success(x);
      this.router.navigateByUrl('polls');
    })
  }

  updatePoll() {
    this.http.post(this.baseUrl + 'answer', this.globalPoll, { responseType: 'text' }).subscribe(x => {
      this.toastr.success(x);
      this.globalPoll = null;
      this.router.navigateByUrl('polls');
    })
  }
}
