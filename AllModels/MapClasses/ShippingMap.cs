using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllModels.MapClasses
{
    public class ShippingMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipping>(entity => entity.ToTable("Shipping"));

            modelBuilder.Entity<Shipping>()
               .HasOne<Country>(x => x.Country)
               .WithMany(x => x.ShippingAddresses)
               .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<Shipping>()
               .HasOne<State>(x => x.State)
               .WithMany(x => x.ShippingAddresses)
               .HasForeignKey(x => x.StateId);
        }
    }
}
