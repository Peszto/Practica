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
    public class ClientMapping :IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Address).IsRequired().HasColumnType("varchar(150)");
            builder.Property(c => c.FirstName).IsRequired().HasColumnType("varchar(150)");
            builder.Property(c => c.LastName).IsRequired().HasColumnType("varchar(150)");
            builder.Property(c => c.PhoneNr).IsRequired().HasColumnType("varchar(150)");
            builder.Property(c => c.Email).IsRequired().HasColumnType("varchar(150)");

            builder.ToTable("Clients");
        }
    }
}
