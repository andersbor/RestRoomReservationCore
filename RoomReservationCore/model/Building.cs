namespace RoomReservationCore.model
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }

        public override string ToString()
        {
            return Id + " " + Name + " " + CityId + " " + Address;
        }
    }
}
