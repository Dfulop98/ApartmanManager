import { Component, OnInit } from '@angular/core';
import { ImageService } from '../../Services/image.service';

@Component({
  selector: 'app-intro-section',
  templateUrl: './intro-section.component.html',
  styleUrls: ['./intro-section.component.css']
})
export class IntroSectionComponent implements OnInit {
  imageUrls!: string[];
  currentImageIndex: number = 0;
  
  constructor(private imageService: ImageService) {}

  ngOnInit() {
    this.imageService.getImages().subscribe((imageUrls) => {
      this.imageUrls = imageUrls;
      setInterval(() => {
        this.currentImageIndex = (this.currentImageIndex + 1) % this.imageUrls.length;
      }, 5000);
    });
  }
}
