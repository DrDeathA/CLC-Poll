import { Component, OnInit } from '@angular/core';
import { PollService } from 'src/app/services/poll.service';

@Component({
  selector: 'app-participate-poll',
  templateUrl: './participate-poll.component.html',
  styleUrls: ['./participate-poll.component.scss']
})
export class ParticipatePollComponent implements OnInit{

  constructor(readonly pollService: PollService) { }

  ngOnInit(): void {
  }

  submitOptions() {
    this.pollService.updatePoll();
  }

}
