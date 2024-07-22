using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Mappings
{
    public class OrderMapping :IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderNr).IsRequired();
            builder.Property(o=>o.Quantity).IsRequired();
            builder.Property(o=>o.TotalPrice).IsRequired();
            builder.Property(o=>o.BookId).IsRequired();
            builder.Property(o=>o.ClientId).IsRequired();

            builder.HasOne(o => o.Book)
                .WithMany()
                .HasForeignKey(o => o.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Orders");

        }
    }
}
