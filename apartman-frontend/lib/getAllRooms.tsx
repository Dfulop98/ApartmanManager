
export default async function getAllRooms() {
    const res = await fetch("http://localhost:5008/api/room");
    if (!res.ok) throw new Error(`failed to fetch`);
    const data = await res.json();
    console.log(data) 
    
    return data;
}