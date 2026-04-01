using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("SerialCheckeds", Schema = "dbo")]
    public class Quarentine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public DateTime DatePackout { get; set; }

        [Required]
        public string TimePassed { get; set; }

        [Required]
        public string UserReader { get; set; }

        [Required]
        public DateTime DateRead { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

