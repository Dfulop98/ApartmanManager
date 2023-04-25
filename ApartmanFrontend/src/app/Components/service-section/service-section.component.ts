import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-service-section',
  templateUrl: './service-section.component.html',
  styleUrls: ['./service-section.component.css']
})
export class ServiceSectionComponent {
  constructor(private router: Router) {}

  sections = [
    {
      title: 'Szállás',
      description: 'Kényelmes szobák, modern berendezéssel és minden igényt kielégítő szolgáltatásokkal.',
      buttonText: 'Foglalás',
      images: ['path/to/room-image-1.jpg', 'path/to/room-image-2.jpg', 'path/to/room-image-3.jpg']
    },
    {
      title: 'Szórakozás, rendezvény',
      description: 'Ideális helyszín esküvők, bulik és egyéb rendezvények számára, különféle szolgáltatásokkal.',
      buttonText: 'Rendezvény foglalása',
      images: ['path/to/event-image-1.jpg', 'path/to/event-image-2.jpg', 'path/to/event-image-3.jpg']
    },
    {
      title: 'Paintball',
      description: 'Izgalmas és szórakoztató paintball élmény a szabadban, tökéletes program barátokkal vagy családdal.',
      buttonText: 'Paintball foglalása',
      images: ['path/to/paintball-image-1.jpg', 'path/to/paintball-image-2.jpg', 'path/to/paintball-image-3.jpg']
    }
  ];

  navigateToBooking() {
    this.router.navigate(['/book']);
  }
}