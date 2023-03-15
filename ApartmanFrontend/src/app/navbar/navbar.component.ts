import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  @Output() activeIndexChanged = new EventEmitter<number>();
  activeIndex: number = 0;

   navItems = [
    { title: 'Home', icon: 'home.png', route: '/home' },
    { title: 'Gallery', icon: 'gallery2.png', route: '/gallery' },
    { title: 'Book', icon: 'book.png', route: '/book' },
    { title: 'Contact', icon: 'contact.png', route: '/contact' },
    { title: 'Support', icon: 'support.png', route: '/support' },
  ];

  setActiveIndex(index: number): void {
    this.activeIndex = index;
    this.activeIndexChanged.emit(this.activeIndex);
  }
}
