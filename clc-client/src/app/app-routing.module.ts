import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ViewPollsComponent } from './components/view-polls/view-polls.component';
import { authGuard } from './guards/auth.guard';
import { CreatePollComponent } from './components/create-poll/create-poll.component';
import { PollResultsComponent } from './components/poll-results/poll-results.component';
import { ParticipatePollComponent } from './components/participate-poll/participate-poll.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: '',
  canActivate: [authGuard],
  children:[
    {path: 'polls', component: ViewPollsComponent},
    {path: 'pollBuilder', component: CreatePollComponent},
    {path: 'pollResults', component: PollResultsComponent},
    {path: 'participate', component: ParticipatePollComponent},
  ]},
  {path: 'login', component: LoginComponent},
  {path: '**', component: LoginComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
