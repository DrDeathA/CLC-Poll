import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ViewPollsComponent } from './components/view-polls/view-polls.component';
import { CreatePollComponent } from './components/create-poll/create-poll.component';
import { PollResultsComponent } from './components/poll-results/poll-results.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ParticipatePollComponent } from './components/participate-poll/participate-poll.component';

import { ToastrModule } from 'ngx-toastr';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';

//Prime
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { DialogModule } from 'primeng/dialog';
import { ChartModule } from 'primeng/chart';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ViewPollsComponent,
    CreatePollComponent,
    PollResultsComponent,
    NavBarComponent,
    ParticipatePollComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    
    BrowserAnimationsModule,
    ToastrModule.forRoot(),

    //Prime
    InputTextModule,
    PasswordModule,
    ButtonModule,
    CardModule,
    DialogModule,
    ChartModule,
    DropdownModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
