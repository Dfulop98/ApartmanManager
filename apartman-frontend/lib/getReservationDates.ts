export default async function getReservationDates(roomId:string) {
    // Send a GET request to the server
  const res = await fetch(`http://localhost:5008/api/Reservation/dates/${roomId}`);
  console.log(res.status)
  // If the response is not ok, throw an error
  if (!res.ok) throw Error(`failed to fetch room`);
  
  // Parse the response as JSON
  const data = await res.json();

  // Return the parsed data
  return data;
}