namespace Airbnb.Api.Models
{
    public class RoomDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int Guests { get; set; }

        public int Adults { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; } = string.Empty;

        public int CurrencyId { get; set; }

        public string CurrencyName { get; set; } = string.Empty;

        public string CurrencyCode { get; set; } = string.Empty;

        public string CurrencySymbol { get; set; } = string.Empty;

        public decimal? Rating { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedByName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
