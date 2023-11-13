import { Component, OnInit } from '@angular/core';
import { PollService } from 'src/app/services/poll.service';

@Component({
  selector: 'app-poll-results',
  templateUrl: './poll-results.component.html',
  styleUrls: ['./poll-results.component.scss']
})
export class PollResultsComponent implements OnInit{
  charts: any[] = [];

  constructor(readonly pollService: PollService) { }

  ngOnInit(): void {
    this.dataExtractor();
  }

  dataExtractor() {
    let labels: string[] = [];
    let data: number[] = [];
    console.log(this.pollService.globalPoll);
    this.pollService.globalPoll?.questions.forEach(question => {

      question.options.forEach(option => {
        labels.push(option.optionText);
        data.push(option.voteCount);
      });

      this.charts.push({
        title: question.title,
        subText: question.subText,
        labels: labels,
        datasets: [
          {
            data: data
          }
        ]
      });
      labels = [];
      data = [];
    });
  }
}
