import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private apiUrl = 'https://localhost:7223/api/room';
  constructor(private http: HttpClient) { }

  getRooms(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
