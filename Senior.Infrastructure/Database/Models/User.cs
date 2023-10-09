using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Infrastructure.Database.Models
{
    public class User
    {
        [Required]
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public bool? CanSell { get; set; }

        public ICollection<Product> Products { get; set; } = null!;
        public ICollection<ProductUser> ProductUsers { get; set; } = null!;
    }
}