namespace RoomReservationCore.model
{
    public class Reservation
    {
        public int Id { get; set; }
        //public DateTime FromTime { get; set; }
        //public DateTime ToTime { get; set; }

        // public string DateString { get; set; }

        public string FromTimeString { get; set; }

        public string ToTimeString { get; set; }
        public string UserId { get; set; }
        public string Purpose { get; set; }

        public int RoomId { get; set; }

        // public string DeviceId { get; set; }

        public Reservation() { }

        //public Reservation(reservation res)
        //{
        //    Id = res.Id;
        //    UserId = res.userId;
        //    //FromTime = res.from.Value;
        //    //ToTime = res.to.Value;
        //    Purpose = null; // TODO missing in database table
        //    RoomId = res.roomId;

        //    // https://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx
        //    FromTimeString = res.from.Value.ToString("dd-MM-yyyy H:mm");

        //    ToTimeString = res.to.Value.ToString("dd-MM-yyyy H:mm");
        //}

        public override string ToString()
        {
            return FromTimeString + " - " + ToTimeString + " by " + UserId;
        }
    }
}
