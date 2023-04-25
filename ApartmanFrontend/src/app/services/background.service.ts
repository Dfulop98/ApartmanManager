import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BackgroundService {
  private showBackgroundSource = new BehaviorSubject(false);
  showBackground$ = this.showBackgroundSource.asObservable();

  toggleBackground(value: boolean): void {
    this.showBackgroundSource.next(value);
  }
}