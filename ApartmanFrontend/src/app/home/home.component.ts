import {Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { faBook } from '@fortawesome/free-solid-svg-icons';
import { faCircleInfo } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  faBook = faBook;
  faCircleInfo = faCircleInfo;
  reservationId!: string;

  onSubmit(form: NgForm): void {
    if (form.valid) {
      // Küldd el az űrlap adatait a szerverre, vagy dolgozd fel őket valamilyen módon
      console.log('reservation Id:', this.reservationId);

      // Ne felejtsd el az űrlapmezőket visszaállítani az alapértelmezett értékekre
      form.resetForm();
    }
  }
  
  
}
