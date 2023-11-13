import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPollsComponent } from './view-polls.component';

describe('ViewPollsComponent', () => {
  let component: ViewPollsComponent;
  let fixture: ComponentFixture<ViewPollsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewPollsComponent]
    });
    fixture = TestBed.createComponent(ViewPollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
