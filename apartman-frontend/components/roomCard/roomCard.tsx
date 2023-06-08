import React from 'react'
import { Room } from "@/types";
import Link from "next/link";
import Styles from "./RoomCard.module.css";
import Carousel from '../carousel/carousel';
export default function RoomCard({params} : {params : Room[]}) {

  return (
    <div className={Styles.container}>

        {params.map(room => {

            return(
                <div className={Styles.room_card} key={room.properties.Id}>
                    <div className={Styles.room_images}>
                        <Carousel params={room.properties.Images} />
                    </div>
                    <div className={Styles.room_info}>
                        <div className={Styles.info_wrapper}>
                            <div className={Styles.info_header}>
                                <Link href={`/rooms/${room.properties.Id}`}>Room{room.properties.RoomNumber}</Link>
                            </div>
                            <div className={Styles.info_body}>
                                <h1>Description:</h1>
                                <p>{room.properties.Description}</p>
                            </div>

                            <div className={Styles.info_footer}>
                                <div className={Styles.footer_wrapper}>
                                    <div className={Styles.footer_details}>
                                        <h1>Capacity:</h1>
                                        <p>{room.properties.Capacity}</p>
                                        <br />
                                        <h1>Price:</h1>
                                        <p>{room.properties.PricePerNight}&#8364;/person</p>
                                    </div>
                                    <div className={Styles.footer_buttons}>
                                        <Link href={`/callendar/${room.properties.Id}`}>Book Now</Link>
                                        <Link href={`/rooms/${room.properties.Id}`}>Calendar</Link>
                                    </div>
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
