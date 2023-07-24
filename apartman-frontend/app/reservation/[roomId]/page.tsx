import { Metadata } from "next";
import Link from "next/link";
import cn from "classnames"
import getRoom from "@/lib/getRoom";

import { Room } from "@/types";

import Styles from "./style.module.css";
import CarouselStyles from "./Carousel.module.css"

import Carousel from "@/components/carousel/carousel";
import ReservationForm from "@/components/reservationForm/reservationForm";

export const metadata: Metadata = {
    title: "Reservation"
}

type params = {
  roomId : string
}


export default async function Reservation({params} : {params : params}) {

  var next_roomid :number = +params.roomId+1;
  const roomData: Promise<Room> = getRoom(params.roomId);
  const room = await roomData;
  

  return (
    <div className={Styles.container}>
      <div className={Styles.calendar_container}>
        <ReservationForm roomId={params.roomId} roomCapacity={room.properties.Capacity} />
      </div>
      <div className={Styles.details_container}>
        <div className={Styles.carousel}>
          <Carousel params={{images: room.properties.Images,styles: CarouselStyles}} />
        </div>
        <div className={Styles.room_details}>
          <h1>Room{room.properties.RoomNumber}</h1>
          
            <p className={Styles.title}>Description</p>
            <p className={Styles.description}>{room.properties.Description}</p>
          
            <p className={Styles.title}>Details</p>
            <div className={Styles.slots}>
              <p >Available slots:</p>
              <p className={Styles.person_icon}> {room.properties.Capacity} (person svg icon)</p>
            </div>
            <div className={Styles.price_wrapper}>
              <p>Price:</p>
              <p className={Styles.price}>{room.properties.PricePerNight}(eurosvg)/(personsvg)/(nightsvg)</p>
            </div>
        </div>
        <div className={Styles.footer_navs}>
          <Link className={Styles.btn} href="/rooms">More</Link>
          <Link className={cn(Styles.btn, Styles.next_room)} href={`/reservation/${next_roomid}`}>Next</Link>
        </div>
      </div>
    </div>
  )
}
