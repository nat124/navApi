using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class CartMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cart>(entity =>
            {

                entity.HasOne(d => d.User)
                  .WithMany(d => d.Carts)
                  .HasForeignKey(d => d.VendorId);
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                
                entity.HasOne(d => d.User)
                  .WithMany(d => d.Carts)
                  .HasForeignKey(d => d.UserId);
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                
                entity.HasMany(d => d.CartItems)
                  .WithOne(d => d.Cart)
                  .HasForeignKey(d => d.CartId);
            });
        }
    }

    public class CartItemMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(m => m.User)
                .WithMany(t => t.CartItems)
                .HasForeignKey(m => m.VendorId);
                   });
                modelBuilder.Entity<CartItem>(entity =>
            {

                entity.HasOne(d => d.Cart)
                  .WithMany(d => d.CartItems)
                  .HasForeignKey(d => d.CartId);
            });
            modelBuilder.Entity<CartItem>(entity =>
            {

                entity.HasOne(d => d.Unit)
                  .WithMany(d => d.CartItems)
                  .HasForeignKey(d => d.UnitId);
            });
            modelBuilder.Entity<CartItem>(entity =>
            {

                entity.HasOne(d => d.ProductVariantDetail)
                  .WithMany(d => d.CartItems)
                  .HasForeignKey(d => d.ProductVariantDetailId);
            });
        }
    }
}
