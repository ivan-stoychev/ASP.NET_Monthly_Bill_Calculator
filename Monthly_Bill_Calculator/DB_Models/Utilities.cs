using Monthly_Bill_Calculator.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Monthly_Bill_Calculator.DB_Models
{
    using static ModelValidation;
    public abstract class Utility
    {
        [Required(ErrorMessage = "Entering consumption is required.")]
        [Range(ConsumptionMin, ConsumptionMax, ErrorMessage = "Consumption must be in the range of 0.0001 - 99999999")]
        public double Consumption { get; set; }

        [Required(ErrorMessage = "Entering a price per unit is required.")]
        [Range(typeof(decimal), PriceMin, PriceMax, ErrorMessage = "price per unit must be in the range of 0.0001 - 99999")]
        public decimal Price { get; set; }

        [NotMapped]
        public decimal MonthlyCost => Price * (decimal)Consumption;
    }
}