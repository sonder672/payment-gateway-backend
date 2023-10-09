using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Infrastructure.Database.Models
{
    public class ProductUser
    {
        [Required]
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public bool Status { get; set; }

        public string? UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public User? User { get; set; } = null!;

        [Required]
        public string ProductId { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public string? Email { get; set; }
    }
}