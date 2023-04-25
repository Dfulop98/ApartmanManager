import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingStatusPageComponent } from './booking-status-page.component';

describe('BookingStatusPageComponent', () => {
  let component: BookingStatusPageComponent;
  let fixture: ComponentFixture<BookingStatusPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookingStatusPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingStatusPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
