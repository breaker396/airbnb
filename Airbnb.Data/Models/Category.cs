using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("Category")]
    public class Category : IdentityKeyEntity<int>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [InverseProperty("Category")]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
