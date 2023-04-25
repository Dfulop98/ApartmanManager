import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeSlideCardsComponent } from './home-slide-cards.component';

describe('HomeSlideCardsComponent', () => {
  let component: HomeSlideCardsComponent;
  let fixture: ComponentFixture<HomeSlideCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeSlideCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeSlideCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
