using Airbnb.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Models
{
    [Table("Country")]
    public class Country : IdentityKeyEntity<int>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [InverseProperty("Country")]
        public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
    }
}
