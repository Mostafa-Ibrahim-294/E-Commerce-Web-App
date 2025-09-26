using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.StreetAddress)
                   .IsRequired(false)
                   .HasMaxLength(200);
            builder.Property(c => c.City)
                   .IsRequired(false)
                   .HasMaxLength(100);
            builder.Property(c => c.State)
                   .IsRequired(false)
                   .HasMaxLength(50);
            builder.Property(c => c.PostalCode)
                   .IsRequired(false)
                   .HasMaxLength(20);
            builder.Property(c => c.PhoneNumber)
                   .IsRequired(false)
                   .HasMaxLength(15);
            builder.HasKey(c => c.Id);
            builder.ToTable("Companies");
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
