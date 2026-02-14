using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class ColdWater : Utility
    {
        [Key]
        public int Id { get; set; }
        public string Unit => "Cubic Meters";
    }
}