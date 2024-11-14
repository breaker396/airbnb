using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("User")]
    public class User : ComputedKeyEntity<long>
    {
        [StringLength(60)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        public string Password { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        [InverseProperty("User")]
        public virtual ICollection<RoomOrder> RoomOrders { get; set; } = new List<RoomOrder>();

    }
}
