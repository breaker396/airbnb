using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    public class RoomOrder : ComputedKeyEntity<long>
    {
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("RoomOrders")]
        public User User { get; set; } = null!;

        public long RoomId { get; set; }

        [ForeignKey("RoomId")]
        [InverseProperty("RoomOrders")]
        public Room Room { get; set; } = null!;

        public int Guests { get; set; }

        public int Adults { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

    }
}
