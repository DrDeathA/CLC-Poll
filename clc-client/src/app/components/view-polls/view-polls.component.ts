import { Component, OnInit } from '@angular/core';
import { PollService } from 'src/app/services/poll.service';
import { Poll } from '../../interfaces/poll';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-polls',
  templateUrl: './view-polls.component.html',
  styleUrls: ['./view-polls.component.scss']
})
export class ViewPollsComponent implements OnInit {
  polls: Poll[] = [];

  constructor(readonly pollService: PollService, readonly router: Router) { }

  ngOnInit(): void {
    this.pollService.viewPolls().subscribe((x: any) => {
      this.polls = x;
    })
  }

  viewPollResults(poll: Poll) {
    this.pollService.globalPoll = poll;
    this.router.navigateByUrl('pollResults');
  }

  participatePoll(poll: Poll) {
    this.pollService.globalPoll = poll;
    this.router.navigateByUrl('participate');
  }

}
