"use client"
import { useState } from "react";
import { ImageType } from "@/types";
import Image from "next/image";
import cn from "classnames";

interface CarouselProps {
  params:{
    images: ImageType[];
    styles: any;
  } 
}

export default function Carousel({ params }: CarouselProps) {
  const [activeIndex, setActiveIndex] = useState(0);
  const {images, styles}  = params;
  const handleButtonClick = (offset: number) => {
    if(activeIndex+offset > images.length-1){
        setActiveIndex(0);
    }
    else if(activeIndex+offset < 0){
        setActiveIndex(images.length-1);
    }
    else{
        setActiveIndex(activeIndex+offset);
    }
    console.log( activeIndex);
    
    
  };

  return (
    <section aria-label="Newest Photos">
      <div className={styles.carousel_background}></div>
      <div className={styles.carousel} data-carousel>
        

          <button
            className={cn(styles.carousel_button, styles.prev)}
            onClick={() => handleButtonClick(-1)}
            >&#8701;</button>

          <button
            className={cn(styles.carousel_button, styles.next)}
            onClick={() => handleButtonClick(1)}
          >&#8702;</button>

          <ul data-slides>
            {images.map((image, index) => (
              <li
                className={cn(styles.slide, { [styles.active]: index === activeIndex })}
                key={image.properties.Id}
                data-active={index === activeIndex ? "" : undefined}
              >
                <Image src={image.properties.Url} alt={`room_${image.properties.Id}`} fill />
              </li>
            ))}
          </ul>
        </div>
      
    </section>
  );
}