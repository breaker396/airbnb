using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("Currency")]
    public class Currency : IdentityKeyEntity<int>
    {
        [StringLength(10)]
        public string Code { get; set; } = null!;
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(10)] 
        public string Symbol { get; set; } = null!;

        [InverseProperty("Currency")]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
