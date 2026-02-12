using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class Steam : Utility
    {
        public Steam()
        {
            Unit = "Cubic Meters";
        }

    }
}