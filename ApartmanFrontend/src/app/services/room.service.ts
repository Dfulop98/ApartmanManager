import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, tap } from 'rxjs';
import { ApiResponse } from '../interfaces/api-response';
import { Room } from '../interfaces/room';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private apiUrl = 'https://localhost:7223/api/room';
  constructor(private http: HttpClient) { }

  getRooms(): Observable<Room[]> {
  return this.http.get<ApiResponse>(this.apiUrl).pipe(
    map(response => response.models as Room[]),);
}
}
