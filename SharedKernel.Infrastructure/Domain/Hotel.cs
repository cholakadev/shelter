namespace SharedKernel.Infrastructure.Domain
{
    public class Hotel
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public HotelStar Stars { get; set; }

        public IEnumerable<Facility> Facilities { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public int MyProperty { get; set; }
    }
}
