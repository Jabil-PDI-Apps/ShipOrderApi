using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("Models")] 
    public class Models
    {
        [Key]
        [Column("PK_Models")]
        public long Id { get; set; }

        [Column("Customer_ID")]
        public long CustomerId { get; set; }

        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string SerialEVAP { get; set; } = string.Empty;

        [StringLength(100)]
        public string SerialCOND { get; set; } = string.Empty;

        [Column("Label_ID")]
        public long? LabelId { get; set; } 

        public bool? Enable { get; set; }

        public bool? IsProductJabil { get; set; }

        public virtual ICollection<OrderModel> OrderModels { get; set; } = new List<OrderModel>();
    }
}
