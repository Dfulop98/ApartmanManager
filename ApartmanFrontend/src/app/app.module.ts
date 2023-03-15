import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomService } from './rooms/rooms.service';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeSlideCardsComponent } from './home-slide-cards/home-slide-cards.component';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    RoomsComponent,
    NavbarComponent,
    HomeSlideCardsComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    RoomService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
