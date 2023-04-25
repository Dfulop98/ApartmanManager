import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { RoomsPageComponent } from './Pages/rooms-page/rooms-page.component';
import { GalleryPageComponent } from './Pages/gallery-page/gallery-page.component';
import { BookingStatusPageComponent } from './Pages/booking-status-page/booking-status-page.component';
import { ContactPageComponent } from './Pages/contact-page/contact-page.component';
import { EventsPageComponent } from './Pages/events-page/events-page.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { IntroSectionComponent } from './Components/intro-section/intro-section.component';
import { ServiceSectionComponent } from './Components/service-section/service-section.component';
import { BuildingSectionComponent } from './Components/building-section/building-section.component';
import { RoomCardComponent } from './Components/room-card/room-card.component';
import { AvailableCalendarComponent } from './Components/available-calendar/available-calendar.component';
import { BookingFormComponent } from './Components/booking-form/booking-form.component';
import { GalleryGridComponent } from './Components/gallery-grid/gallery-grid.component';
import { BookingStatusFormComponent } from './Components/booking-status-form/booking-status-form.component';
import { ContactInfoComponent } from './Components/contact-info/contact-info.component';
import { SupportFormComponent } from './Components/support-form/support-form.component';
import { EventSectionComponent } from './Components/event-section/event-section.component';
import { EventCardComponent } from './Components/event-card/event-card.component';
import { ExactActiveLinkDirective } from './Directives/exact-active-link.directive';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    RoomsPageComponent,
    GalleryPageComponent,
    BookingStatusPageComponent,
    ContactPageComponent,
    EventsPageComponent,
    IntroSectionComponent,
    ServiceSectionComponent,
    BuildingSectionComponent,
    RoomCardComponent,
    AvailableCalendarComponent,
    BookingFormComponent,
    GalleryGridComponent,
    BookingStatusFormComponent,
    ContactInfoComponent,
    SupportFormComponent,
    EventSectionComponent,
    EventCardComponent,
    ExactActiveLinkDirective,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }   
