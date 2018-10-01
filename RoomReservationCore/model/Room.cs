namespace RoomReservationCore.model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Remarks { get; set; }
        public int BuildingId { get; set; }
    }
}
