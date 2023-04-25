import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faImages } from '@fortawesome/free-solid-svg-icons';
import {faHome, faBook, faHeadset, faPhoneFlip } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent   {
  
  constructor(private router: Router) {}

  @Output() activeIndexChanged = new EventEmitter<number>();
  activeIndex: number = 0;
  iconColor: string = 'white'
  navItems = [
    { title: 'Home', icon: faHome, route: '/Home' },
    { title: 'Gallery', icon: faImages as IconProp, route: '/Gallery' },
    { title: 'Book', icon: faBook as IconProp, route: '/book' },
    { title: 'Contact', icon: faPhoneFlip as IconProp, route: '/contact' },
    { title: 'Support', icon: faHeadset as IconProp, route: '/support' },
  ];

  setActiveIndex(index: number): void {
    this.activeIndex = index;
    this.activeIndexChanged.emit(this.activeIndex);
    this.router.navigate([this.navItems[index].route]);
  }



  
}
