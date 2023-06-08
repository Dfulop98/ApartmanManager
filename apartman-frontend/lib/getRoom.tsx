
export default async function getRoom(roomId :string) {

  const res = await fetch(`http://localhost:5008/api/room/${roomId}`);

  if(!res.ok) throw new Error('failed to fetch');
  return res.json()
}
