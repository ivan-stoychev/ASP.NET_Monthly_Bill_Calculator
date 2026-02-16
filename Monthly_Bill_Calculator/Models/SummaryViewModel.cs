using Monthly_Bill_Calculator.Common;
using System.ComponentModel.DataAnnotations;

namespace Monthly_Bill_Calculator.Models
{
    using static ModelValidation;
    public class UtilitySummary
    {
        public double? AvgConsumption { get; set; }
        public decimal AvgPrice { get; set; }

        public double? TotalConsumption { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class SummaryViewModel
    {
        [Required]
        [Range(MinYear, MaxYear)]
        public int StartYear { get; set; }

        [Required]
        [Range(MinMonth, MaxMonth)]
        public int StartMonth { get; set; }

        [Required]
        [Range(MinYear, MaxYear)]
        public int EndYear { get; set; }

        [Required]
        [Range(MinMonth, MaxMonth)]
        public int EndMonth { get; set; }

        // Stores the average consumption and price for each utility type, as well as the total consumption and price for the specified period.
        public Dictionary<string, UtilitySummary>? Utilities { get; set; }

        // Total amount spent across all utilities for the specified period.
        public decimal TotalSpent { get; set; }

    }
}