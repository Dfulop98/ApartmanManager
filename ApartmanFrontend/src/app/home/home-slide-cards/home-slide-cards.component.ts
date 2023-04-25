import { Component,Input, OnInit } from '@angular/core';
import Swiper from 'swiper';
import { RoomService } from '../../services/room.service';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { map, Observable } from 'rxjs';
import { ApiResponse } from '../../interfaces/api-response';
import { Room } from '../../interfaces/room';

@Component({
  selector: 'app-home-slide-cards',
  templateUrl: './home-slide-cards.component.html',
  styleUrls: ['./home-slide-cards.component.css']
})

export class HomeSlideCardsComponent implements OnInit {
  rooms$: Observable<Room[]>
  faUser = faUser;
  @Input() activeIndex?: number; 
  swiper?: Swiper;
  
  constructor(private roomService: RoomService) {
    this.rooms$ = this.roomService.getRooms()
  }

  ngOnInit(): void {
    this.initSwiper();
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
