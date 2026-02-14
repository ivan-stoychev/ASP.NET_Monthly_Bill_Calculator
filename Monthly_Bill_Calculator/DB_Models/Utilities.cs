using Monthly_Bill_Calculator.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Monthly_Bill_Calculator.DB_Models
{
    using static ModelValidation;
    public abstract class Utility
    {
        [Required]
        [Range(ConsumptionMin, ConsumptionMax)]
        public double Consumption { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal Price { get; set; }

        [NotMapped]
        public decimal MonthlyCost => Price * (decimal)Consumption;
    }
}