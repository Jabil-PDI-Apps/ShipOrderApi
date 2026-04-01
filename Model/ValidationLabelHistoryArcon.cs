using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipOrderBack.Model
{
    [Table("ValidationLabelHistoryArcon", Schema = "dbo")]
    public class ValidationLabelHistoryArcon
    {
        [Key]
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public int AssemblyID { get; set; }

        [StringLength(100)]
        public string? AssemblyNo { get; set; }

        [StringLength(50)]
        public string? AssemblyRev { get; set; }

        [StringLength(200)]
        public string? LabelName { get; set; }

        [StringLength(50)]
        public string? LabelType { get; set; }

        public string? UserID { get; set; }

        public DateTime? PrintDate { get; set; }

        [StringLength(100)]
        public string? SerialNumber { get; set; }

        [StringLength(100)]
        public string? SerialNumberTemp { get; set; }

        // CORREÇÃO CRUCIAL: Mudar para string porque o valor é "PASS" ou "FAIL"
        [StringLength(50)]
        public string? Status { get; set; }

        public int? OrderLabel { get; set; }
    }
}