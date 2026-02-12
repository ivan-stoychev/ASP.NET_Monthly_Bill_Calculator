using Monthly_Bill_Calculator.DB_Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.Common
{
    public class Month
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(1, 12)]
        public int MonthNumber { get; set; }

        // Foreign keys
        public int? ElectricityId { get; set; }
        public int? ColdWaterId { get; set; }
        public int? HotWaterId { get; set; }
        public int? NaturalGasId { get; set; }
        public int? SteamId { get; set; }
        public int? CentralHeatingId { get; set; }

        // Navigation properties
        public Electricity Electricity { get; set; }
        public ColdWater ColdWater { get; set; }
        public HotWater HotWater { get; set; }
        public NaturalGas NaturalGas { get; set; }
        public Steam Steam { get; set; }
        public CentralHeating CentralHeating { get; set; }

        // Calculated total cost for the month
        [NotMapped]
        public decimal Total =>
            (Electricity?.MonthlyCost ?? 0) +
            (ColdWater?.MonthlyCost ?? 0) +
            (HotWater?.MonthlyCost ?? 0) +
            (NaturalGas?.MonthlyCost ?? 0) +
            (Steam?.MonthlyCost ?? 0) +
            (CentralHeating?.MonthlyCost ?? 0);
    }

}
