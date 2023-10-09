using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Infrastructure.Database.Models;

namespace Senior.Infrastructure.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<ProductUser> ProductUser { get; set; } = null!;
        public DbSet<Membership> Membership { get; set; } = null!;
    }
}