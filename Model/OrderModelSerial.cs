using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("OrderModelSerial")]
    public class OrderModelSerial
    {
        [Key]
        public long OrderModelSerial_ID { get; set; }

        [Required]
        public long FK_OrderModel { get; set; }

        [Required]
        [StringLength(100)]
        public string? SerialModel { get; set; }

        [Required]
        [StringLength(100)]
        public string? Serial { get; set; }

        [Required]
        public long UserReader { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        public bool? IsStuffed { get; set; }

        [ForeignKey("FK_OrderModel")]
        public virtual OrderModel OrderModel { get; set; }
    }
}
