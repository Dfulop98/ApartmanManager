"use client"
import { useState } from "react";
import { ImageType } from "@/types";
import Styles from "./Carousel.module.css";
import Image from "next/image";
import cn from "classnames";

interface CarouselProps {
  params: ImageType[];
}

export default function Carousel({ params }: CarouselProps) {
  const [activeIndex, setActiveIndex] = useState(0);

  const handleButtonClick = (offset: number) => {
    if(activeIndex+offset > params.length-1){
        setActiveIndex(0);
    }
    else if(activeIndex+offset < 0){
        setActiveIndex(params.length-1);
    }
    else{
        setActiveIndex(activeIndex+offset);
    }
    console.log( activeIndex);
    
    
  };

  return (
    <section aria-label="Newest Photos">
      <div className={Styles.carousel} data-carousel>
        
        <button
          className={cn(Styles.carousel_button, Styles.prev)}
          onClick={() => handleButtonClick(-1)}
          >&#8701;</button>

        <button
          className={cn(Styles.carousel_button, Styles.next)}
          onClick={() => handleButtonClick(1)}
        >&#8702;</button>

        <ul data-slides>
          {params.map((image, index) => (
            <li
              className={cn(Styles.slide, { [Styles.active]: index === activeIndex })}
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