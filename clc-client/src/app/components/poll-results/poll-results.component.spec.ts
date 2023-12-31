import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PollResultsComponent } from './poll-results.component';

describe('PollResultsComponent', () => {
  let component: PollResultsComponent;
  let fixture: ComponentFixture<PollResultsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PollResultsComponent]
    });
    fixture = TestBed.createComponent(PollResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
