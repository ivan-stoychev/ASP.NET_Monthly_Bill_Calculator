using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class NaturalGas : Utility
    {
        [Key]
        public int Id { get; set; }
        public NaturalGas()
        {
            Unit = "Cubic Meters";
        }
    }
}