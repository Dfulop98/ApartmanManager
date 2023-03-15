import { Component, ViewChild } from '@angular/core';
import { NavbarComponent } from './navbar/navbar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ApartmanFrontend';
  @ViewChild(NavbarComponent) navbarComponent!: NavbarComponent;

  activeIndex: number = 0;

  onActiveIndexChanged(index: number): void {
    this.activeIndex = index;
  }
}
