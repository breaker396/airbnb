namespace Airbnb.Api.Models
{
    public class RoomOrderDto
    {
        public long RoomId { get; set; }
        public int Guests { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

    }
}
