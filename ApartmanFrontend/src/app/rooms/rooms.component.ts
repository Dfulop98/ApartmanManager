import { Component } from '@angular/core';
import { RoomService } from './rooms.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent {
  rooms: any;

  constructor(service: RoomService) {
    this.rooms = service.getRooms();
  }
}
