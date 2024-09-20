using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace RestauranteApi.Models
{
    
        public class ApiContext : DbContext
        {
            public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
            public DbSet<Apiresta> Produtos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Apiresta>().ToTable("produtos");
            }
        }
    
}
