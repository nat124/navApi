using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class CompareProductMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompareProduct>(e => e.ToTable("CompareProduct"));
            modelBuilder.Entity<CompareProduct>()
               .HasOne<User>(x => x.User)
               .WithMany(x => x.CompareProducts)
               .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<CompareProduct>()
              .HasOne<ProductVariantDetail>(x => x.ProductVariantDetail)
              .WithMany(x => x.CompareProducts)
              .HasForeignKey(x => x.ProductVariantDetailId);
        }
    }
}
