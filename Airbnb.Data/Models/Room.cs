using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("Room")]
    public class Room : ComputedKeyEntity<long> , ICRUDEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; } = null!;

        public int Guests { get; set; }

        public int Adults { get; set; }

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Rooms")]
        public User User { get; set; } = null!;

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Rooms")]
        public Category Category { get; set; } = null!;


        [Column(TypeName = "decimal(19, 6)")]
        public decimal Price { get; set; }

        public int CurrencyId { get; set; }


        [ForeignKey("CurrencyId")]
        [InverseProperty("Rooms")]
        public Currency Currency { get; set; } = null!;

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? Rating { get; set; }

        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        [InverseProperty("Rooms")]
        public Province Province { get; set; } = null!;

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        [InverseProperty("Room")]
        public virtual ICollection<RoomOrder> RoomOrders { get; set; } = new List<RoomOrder>();

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long DeleteBy { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
