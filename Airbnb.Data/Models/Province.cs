using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("Province")]
    public class Province : IdentityKeyEntity<int>
    {
        [StringLength(100)]
        public string Name {  get; set; } = null!;

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Provinces")]
        public Country Country { get; set; } = null!;

        [InverseProperty("Province")]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
