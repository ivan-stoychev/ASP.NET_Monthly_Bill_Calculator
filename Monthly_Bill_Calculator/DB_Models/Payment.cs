using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int BillId { get; set; }
        public Bill Bill { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        public CalcAppUser User { get; set; } = null!;

        [Required]
        public DateTime PaidOn { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string? Method { get; set; }
    }
}