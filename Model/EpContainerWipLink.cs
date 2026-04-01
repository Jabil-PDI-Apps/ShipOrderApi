using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("EP_ContainerWipLink", Schema = "dbo")]
    public class EpContainerWipLink
    {
        // Chave Estrangeira para WpWip
        [Required]
        public int Wip_ID { get; set; }

        [ForeignKey("Wip_ID")] // Indica que WpWip usa a coluna Wip_ID
        public virtual WpWip WpWip { get; set; }

        [Required]
        // Removi o [ForeignKey("Container")] pois não há propriedade "Container" nesta classe
        public int Container_ID { get; set; }

        [Required]
        public int RouteStep_ID { get; set; }

        [Required]
        public int Equipment_ID { get; set; }

        [Required]
        public int UserID_ID { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
