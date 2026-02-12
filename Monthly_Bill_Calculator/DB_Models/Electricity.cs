using Monthly_Bill_Calculator.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class Electricity : Utility
    {
        public Electricity()
        {
            Unit = "kW/h";
        }
    }
}