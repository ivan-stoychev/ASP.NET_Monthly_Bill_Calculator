using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Monthly_Bill_Calculator.DB_Models
{
    public class CalcAppUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}