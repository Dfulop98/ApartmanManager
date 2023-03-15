import { Component,Input, OnInit } from '@angular/core';
import Swiper from 'swiper';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-home-slide-cards',
  templateUrl: './home-slide-cards.component.html',
  styleUrls: ['./home-slide-cards.component.css']
})

export class HomeSlideCardsComponent implements OnInit {
  @Input() activeIndex?: number;
  swiper?: Swiper;

  rooms: any[] = [];
  
  constructor(private roomService: RoomService) {}

  ngOnInit(): void {
    this.fetchRooms();
  }

  fetchRooms(): void {
    this.roomService.getRooms().subscribe(
      (data: unknown) => {
        if (typeof data === 'object' && data !== null && 'models' in data) {
          this.rooms = (data as any).models;
          console.log((data as any).models);
          this.initSwiper();
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching rooms:', error);
      }
    );
  }

  initSwiper(): void {
    this.swiper = new Swiper('.swiper-container', {
      slidesPerView: 'auto',
      spaceBetween: 10,
      centeredSlides: true,
      touchMoveStopPropagation: false,
      touchReleaseOnEdges: true,
      pagination: {
        el: '.swiper-pagination',
        clickable: true
      }
      
    });
  }
}
