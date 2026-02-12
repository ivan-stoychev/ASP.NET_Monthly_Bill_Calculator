using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class Electricity : Utility
    {
        [Key]
        public int Id { get; set; }
        public Electricity()
        {
            Unit = "kW/h";
        }
    }
}