using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("OrderModel")]
    public class OrderModel
    {
        [Key]
        public long PK_OrderModel { get; set; }

        [Required]
        public long FK_Order { get; set; }

        [Required]
        public long FK_Model { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("FK_Model")]
        public virtual Models Model { get; set; } = null!;
        public virtual ICollection<OrderModelSerial> OrderModelSerials { get; set; } = new List<OrderModelSerial>();
    }
}
