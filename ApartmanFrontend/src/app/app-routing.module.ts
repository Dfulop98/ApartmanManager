import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Importálja a komponenseket, amelyekhez útvonalakat szeretne hozzáadni
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { RoomsPageComponent } from './Pages/rooms-page/rooms-page.component';
// További komponensek...

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'rooms', component: RoomsPageComponent },
  // További útvonalak...
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }