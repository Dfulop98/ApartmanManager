import React from 'react'
import { Room } from "@/types";
import Link from "next/link";
import Styles from "./RoomCard.module.css";
import CarouselStyles from "./Carousel.module.css"
import Carousel from '../carousel/carousel';
import cn from "classnames";
export default function RoomCard({params} : {params : Room[]}) {

  return (
    <div className={Styles.container}>

        {params.map(room => {

            return(
                <div key={room.properties.Id} className={Styles.room_container}>

                    <div className={Styles.room_card} >
                        <div className={Styles.room_images}>
                            <Carousel params={{images: room.properties.Images,styles: CarouselStyles}} />
                        </div>
                        <div className={cn(Styles.room_info,Styles.info_wrapper)}>
                            <div className={Styles.info_header}>
                                <Link href={`/rooms/${room.properties.Id}`}>Room{room.properties.RoomNumber}</Link>
                            </div>
                            <div className={Styles.info_body}>
                                <h1>Description:</h1>
                                <p>{room.properties.Description}</p>
                            </div>

                            <div className={cn(Styles.info_footer,Styles.footer_wrapper)}>
                                
                                <div className={Styles.footer_details}>
                                    <h1>Capacity:</h1>
                                    <p>{room.properties.Capacity}</p>
                                    <br />
                                    <h1>Price:</h1>
                                    <p>{room.properties.PricePerNight}&#8364;/person</p>
                                </div>
                                <div className={Styles.footer_buttons}>
                                    <Link href={`/reservation/${room.properties.Id}`}>Book Now</Link>
                                    <Link href={`/rooms/${room.properties.Id}`}>Calendar</Link>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
                )
            })}
        </div>
  )
}
