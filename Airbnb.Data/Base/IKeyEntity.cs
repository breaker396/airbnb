using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airbnb.Data.Base
{
    public abstract class IdentityKeyEntity<TId> : IKeyEntity<TId>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        [Key]
        public TId Id { get; set; }
    }
    public abstract class ComputedKeyEntity<TId> : IKeyEntity<TId>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("ID")]
        [Key]
        public TId Id { get; set; }
    }
    public interface IKeyEntity<TId>
    {
        public TId Id { get; set; }
    }
    public interface IBaseEntity { }
    public interface ICRUDEntity<TId>
    {
        public TId CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public TId ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public TId DeleteBy { get; set; }

        public bool? IsDeleted { get; set; }
    }

}
