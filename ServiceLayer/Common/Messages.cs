namespace ServiceLayer.Common
{
    public static class Messages
    {


        public const string RoomGetOk = "The room successfully returned.";
        public const string RoomsGetOk = "The rooms successfully returned.";
        public const string RoomCreated = "The room successfully added.";
        public const string RoomUpdated = "The room successfully updated.";
        public const string RoomDeleted = "Room successfully deleted.";


        public const string RoomNotFound = "The room is doesnt exists.";
        public const string RoomConflict = "The room already exists.";


        public const string ImagesCreated = "The picture succesfully added.";

        public const string ReservationGetOk = "The Reservation successfully returned.";
        public const string ReservationsGetOk = "The Reservations successfully returned.";
        public const string ReservationCreated = "The Reservation successfully added.";
        public const string ReservationUpdated = "The Reservation successfully updated.";
        public const string ReservationDeleted = "Reservation successfully deleted.";
        public const string ReservationRoomConflict = "The Room is not available.";


        public const string ReservationNotFound = "The Reservation is doesnt exists.";
        public const string ReservationConflict = "The Reservation already exists.";
        public const string ReservationRoomBadRequest = "The Room doesnt exists.";
        public const string ReservationDateBadRequest = "The Reservation date is incorrect.";

    }
}




