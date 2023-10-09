using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Infrastructure.Database.Models
{
    public class Product
    {
        [Required]
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public string? Image { get; set; } = null!;

        public int? Stock { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public ProductType Type { get; set; }

        public PaymentPeriod? PaymentPeriod { get; set; }

        public ICollection<ProductUser> ProductUsers { get; set; } = null!;
    }
}