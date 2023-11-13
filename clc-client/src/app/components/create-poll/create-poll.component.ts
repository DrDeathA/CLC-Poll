import { Component } from '@angular/core';
import { Poll } from 'src/app/interfaces/poll';
import { Question } from 'src/app/interfaces/question';
import { PollService } from 'src/app/services/poll.service';

@Component({
  selector: 'app-create-poll',
  templateUrl: './create-poll.component.html',
  styleUrls: ['./create-poll.component.scss']
})
export class CreatePollComponent {
  poll: Poll = {
    pollId: 0,
    topic: '',
    questions:[]
  }
  tempQuestion: Question = {
    questionId: 0,
    title: '',
    subText: '',
    options: [],
    answerOptionId: null
  }
  selectedQuestion: Question = {
    questionId: 0,
    title: '',
    subText: '',
    options: [],
    answerOptionId: null
  };
  option: string = '';
  showAddQuestionDialog: boolean = false;
  showAddOptionDialog: boolean = false;

  constructor(readonly pollService: PollService) { }

  showAddQuestion() {
    this.showAddQuestionDialog = true;
  }

  showAddOption() {
    this.showAddOptionDialog = true;
  }

  resetAddQuestion() {
    this.tempQuestion = {
      questionId: 0,
      title: '',
      subText: '',
      options: [],
      answerOptionId: null
    }
  }

  addQuestion(){
    this.poll.questions.push(this.tempQuestion);
    this.resetAddQuestion();
    this.showAddQuestionDialog = false;
  }

  resetSelectedQuestion(){
    this.selectedQuestion = {
      questionId: 0,
      title: '',
      subText: '',
      options: [],
      answerOptionId: null
    };
  }

  selectQuestion(question: Question) {
    this.selectedQuestion = question;
    this.showAddOptionDialog = true;
  }

  addOption() {
    this.selectedQuestion.options.push({
      optionId:0,
      voteCount: 0,
      optionText: this.option
    });
    this.option = '';
  }

  save() {
    this.pollService.saveNewPoll(this.poll);
  }
}
