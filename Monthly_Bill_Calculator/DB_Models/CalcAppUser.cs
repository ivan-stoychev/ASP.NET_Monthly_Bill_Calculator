using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class CalcAppUser : IdentityUser
    {
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

    }
}