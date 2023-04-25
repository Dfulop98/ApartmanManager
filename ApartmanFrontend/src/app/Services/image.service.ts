import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResponse, Image } from '../Models/images';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private apiUrl = 'https://localhost:7223/api/images';

  constructor(private http: HttpClient) {}
  
  getImages(): Observable<string[]> {
    return this.http.get<ApiResponse>(this.apiUrl).pipe(
      map((response) => {
        return response.models.map((model) => model.properties.Url);
      })
    );
  }
}
