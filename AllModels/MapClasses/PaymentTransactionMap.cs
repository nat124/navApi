using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class PaymentTransactionMap : IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PaymentTransaction>(entity =>
            //{
            ////    entity.ToTable("PaymentTransaction");
            //   //entity.HasOne(d => d.User)
            //      .WithMany()
            //      .HasForeignKey(d => d.UserId);
            //});
        }
    }
}
