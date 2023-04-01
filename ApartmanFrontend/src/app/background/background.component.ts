import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { BackgroundService } from '../services/background.service';

@Component({
  selector: 'app-background',
  templateUrl: './background.component.html',
  styleUrls: ['./background.component.css']
})
export class BackgroundComponent {
  showBackground = false;
  private subscription!: Subscription;

  constructor(private backgroundService: BackgroundService) {}

  ngOnInit(): void {
    this.subscription = this.backgroundService.showBackground$.subscribe(
      show => {
        this.showBackground = show;
      }
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
