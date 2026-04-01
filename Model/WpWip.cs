using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("WP_Wip", Schema = "dbo")]
    public class WpWip
    {
        [Key]
        public int Wip_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        [Required]
        public int Customer_ID { get; set; }

        [Required]
        public short SeqNumber { get; set; }

        [Required]
        public int Panel_ID { get; set; }

        [Required]
        public byte Panel { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Purge { get; set; }

        [Required]
        public bool ReferenceUnit { get; set; }

        [Required]
        public bool ReferenceUnitActive { get; set; }

        [Required]
        public bool Scrap { get; set; }

        [Required]
        public bool OnHold { get; set; }

        [Required]
        public bool CurrentlyOnHold { get; set; }

        [Required]
        public bool RMA { get; set; }

        [Required]
        public short RMAReturnCount { get; set; }

        [Required]
        public bool Deviation { get; set; }

        [Required]
        [StringLength(100)]
        public string MultiPartBarcode { get; set; }

        [Required]
        public bool BoardPanelBroken { get; set; }

        [Required]
        public byte BirthStatus { get; set; }

        [Required]
        public int UserID_ID { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<EpContainerWipLink> ContainerLinks { get; set; }
    }
}
