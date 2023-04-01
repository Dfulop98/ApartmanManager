import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomService } from './rooms/rooms.service';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeSlideCardsComponent } from './home/home-slide-cards/home-slide-cards.component';
import { HttpClientModule } from '@angular/common/http';
import { UploadImageComponent } from './upload-image/upload-image.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BackgroundComponent } from './background/background.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { GalleryComponent } from './gallery/gallery.component';
import { FormsModule } from '@angular/forms';

const appRoute: Routes = [
  { path: 'Home', component: HomeComponent },
  { path: 'Gallery', component: GalleryComponent },
]
@NgModule({
  declarations: [
    AppComponent,
    RoomsComponent,
    NavbarComponent,
    HomeSlideCardsComponent,
    UploadImageComponent,
    BackgroundComponent,
    HomeComponent,
    GalleryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    RouterModule.forRoot(appRoute),
    FormsModule
  ],
  providers: [
    RoomService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
