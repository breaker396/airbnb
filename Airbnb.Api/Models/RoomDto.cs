namespace Airbnb.Api.Models
{
    public class RoomDto
    {
        public string Name { get; set; } = null!;

        public int Guests { get; set; }

        public int Adults { get; set; }

        public long UserId { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int CurrencyId { get; set; }

        public decimal? Rating { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
