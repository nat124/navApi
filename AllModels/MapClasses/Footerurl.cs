using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class FooterurlMap:IMap
    {
        public void execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FooterUrl>(entity=>entity.ToTable ("FooterUrl"));
            modelBuilder.Entity<FooterUrl>()
                .HasOne<FooterHeader>(x => x.FooterHeader)
                .WithMany(x => x.FooterUrls)
                .HasForeignKey(x => x.FooterHeaderId);
        }

    }
}
