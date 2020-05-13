using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class CategoryMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");
                entity.HasOne(d => d.Parent)
                  .WithMany()
                  .HasForeignKey(d=>d.ParentId);
            });
            //modelBuilder.Entity<ProductCategory>().Property("ProductCategory");
            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(x => x.Parent)
            //    .WithMany()
            //    .HasForeignKey(x => x.ParentId);
        }
    }

    public class CategoryVariantMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CategoryVariant>().Property("CategoryVariant");
            modelBuilder.Entity<CategoryVariant>(entity =>
            {
                entity.HasOne(x => x.Variant)
                .WithMany(x => x.CategoryVariants)
                .HasForeignKey(x => x.VariantId);
            });

            modelBuilder.Entity<CategoryVariant>(entity=> {
                entity.HasOne(x => x.ProductCategory)
                .WithMany(x => x.CategoryVariants)
                .HasForeignKey(x => x.ProductCategoryId);
            });

            modelBuilder.Entity<CategoryVariant>(entity => {
                entity.HasMany(x => x.ProductVariantOptions)
                .WithOne(x => x.CategoryVariant)
                .HasForeignKey(x => x.CategoryVariantId);
            });
        }

    }
}
