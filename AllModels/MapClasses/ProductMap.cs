using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductMap:IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e=>e.ToTable("Product"));
            modelBuilder.Entity<Product>()
                .HasOne<ProductCategory>(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId);

            modelBuilder.Entity<Product>()
               .HasOne<Unit>(x => x.Unit)
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.UnitId);

            modelBuilder.Entity<Product>()
               .HasMany<ProductImage>(x => x.ProductImages)
               .WithOne(x => x.Product)
               .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<Product>()
              .HasMany<ProductVariantDetail>(x => x.ProductVariantDetails)
              .WithOne(x => x.Product)
              .HasForeignKey(x => x.ProductId);
        }
    }

    public class ProductVariantOptionMap:IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVariantOption>(e=>e.ToTable("ProductVariantOption"));
            modelBuilder.Entity<ProductVariantOption>()
                .HasOne<ProductVariantDetail>(x => x.ProductVariantDetail)
                .WithMany(x => x.ProductVariantOptions)
                .HasForeignKey(x => x.ProductVariantDetailId);

            modelBuilder.Entity<ProductVariantOption>()
               .HasOne<CategoryVariant>(x => x.CategoryVariant)
               .WithMany(x => x.ProductVariantOptions)
               .HasForeignKey(x => x.CategoryVariantId);

            modelBuilder.Entity<ProductVariantOption>()
               .HasOne<VariantOption>(x => x.VariantOption)
               .WithMany(x => x.ProductVariantOptions)
               .HasForeignKey(x => x.VariantOptionId);

            modelBuilder.Entity<ProductVariantOption>()
              .HasMany<StockTransaction>(x => x.StockTransactions)
              .WithOne(x => x.ProductVariantOption)
              .HasForeignKey(x => x.ProductVariantOptionId);

            modelBuilder.Entity<ProductVariantDetail>()
            .HasMany(x => x.WishLists)
            .WithOne(x => x.ProductVariantDetail)
            .HasForeignKey(x => x.ProductVariantDetailId);
        }
    }

    public class ProductImageMap:IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>().Property("ProductImage");
            modelBuilder.Entity<ProductImage>()
                .HasOne<Product>(x => x.Product)
                .WithMany(x => x.ProductImages)
                .HasForeignKey(x => x.ProductId);

           
        }
    }

    public class ProductVariantDetailMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVariantDetail>().Property("ProductVariantDetail");
            modelBuilder.Entity<ProductVariantDetail>()
                .HasOne<Product>(x => x.Product)
                .WithMany(x => x.ProductVariantDetails)
                .HasForeignKey(x => x.ProductId);


            modelBuilder.Entity<ProductVariantDetail>()
                .HasMany(x => x.ProductVariantOptions)
                .WithOne(x => x.ProductVariantDetail)
                .HasForeignKey(x => x.ProductVariantDetailId);
            modelBuilder.Entity<ProductVariantDetail>()
               .HasMany(x => x.CartItems)
               .WithOne(x => x.ProductVariantDetail)
               .HasForeignKey(x => x.ProductVariantDetailId);

        }
    }

    public class FeatureProductMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureProduct>().Property("FeatureProduct");
            modelBuilder.Entity<FeatureProduct>()
                .HasOne<Product>(x => x.Product)
                .WithMany(x => x.FeatureProducts)
                .HasForeignKey(x => x.ProductId);


        }
    }
}
