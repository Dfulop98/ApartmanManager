import { Day, FormValues } from "@/types";

export default async function sendReservationRequest(data: FormValues, checkInDate: Day|null, checkOutDate: Day|null, roomId: string) {
    
    const response = await fetch('http://localhost:5008/api/reservation/request', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            name: `${data.firstName} ${data.lastName}`,
            email: data.email,
            phone: data.phone,
            checkInDate: checkInDate?.date,
            checkOutDate: checkOutDate?.date,
            numberOfGuest: data.numberOfGuest,
            roomId: roomId,

        }),
    });

    return await response.json();
}