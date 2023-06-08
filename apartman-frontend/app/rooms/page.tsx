import { Metadata } from "next";

import getAllRooms from "@/lib/getAllRooms";
import { Room } from "@/types";

import RoomCard from "@/components/roomCard/roomCard";
 
export const metadata: Metadata = {
    title: "Rooms"
}
export default async function RoomsPage() {
    const roomsData: Promise<Room[]> =  getAllRooms();
    const rooms = await roomsData;

    return (
        <RoomCard params={rooms} />
    )

}