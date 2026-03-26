using Microsoft.AspNetCore.Identity;
using Monthly_Bill_Calculator.DB_Models;
using System.ComponentModel.DataAnnotations;

namespace Monthly_Bill_Calculator.DB_MModels
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        // Optional: link to bills
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        // Optional: link to payments
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}