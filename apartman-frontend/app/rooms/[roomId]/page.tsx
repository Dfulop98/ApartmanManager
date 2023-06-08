import { Params } from "@/types";

export default function RoomsPage({ params: {roomId}}: Params) {
  return (
    <div>{roomId}</div>
  )
}
