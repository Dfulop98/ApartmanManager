import { Metadata } from "next";
import getAllRoom from "@/lib/getAllRoom";
import Link from "next/link";

export const metadata: Metadata = {
    title: "Rooms"
}
export default async function RoomsPage() {
    const roomsData: Promise<Room[]> = getAllRoom();
    const rooms = await roomsData;
    console.log(rooms)
    const content = (
        <section>
            <h2>
                <Link href="/"> back to home</Link>
            </h2>
            <br />
            {rooms.map(room => {
                return(
                    <>
                        <p key={room.properties.Id}>
                            <Link href={`/rooms/${room.properties.Id}`}> {room.properties.Description}</Link>
                        </p>
                    </>
                )
                })}
        </section>
    )
    return content
}