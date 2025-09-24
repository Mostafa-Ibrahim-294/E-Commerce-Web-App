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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(p => p.ISBN)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(p => p.Description)
                   .HasMaxLength(1000)
                     .IsRequired(false);
            builder.Property(p => p.Author)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.ListPrice)
                   .IsRequired()
                   .HasColumnType("decimal")
                   .HasPrecision(8, 2);
            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasPrecision(8, 2);

            builder.Property(p => p.Price50)
                   .IsRequired()
                   .HasPrecision(10, 2);
            builder.Property(p => p.Price100)
                   .IsRequired()
                   .HasPrecision(8, 2);
            builder.HasKey(p => p.Id);
            builder.ToTable("Products");
            builder.Property(builder => builder.Id).ValueGeneratedOnAdd();
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Title = "Whispers of the Forest",
                    Author = "Billy Spark",
                    Description = "A calming journey through the woods, where every tree tells a story of peace and resilience.",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = string.Empty
                },
                new Product
                {
                    Id = 2,
                    Title = "Shadows of the Past",
                    Author = "Nancy Hoover",
                    Description = "An epic tale of hidden secrets that resurface to challenge the bonds of friendship and trust.",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1 ,
                    ImageUrl = string.Empty
                },
                new Product
                {
                    Id = 3,
                    Title = "Echoes of Tomorrow",
                    Author = "Julian Button",
                    Description = "A futuristic adventure exploring the thin line between human dreams and technological reality.",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 3 ,
                    ImageUrl = string.Empty
                },
                new Product
                {
                    Id = 4,
                    Title = "The Hidden Valley",
                    Author = "Abby Muscles",
                    Description = "A thrilling story of explorers stumbling upon a forgotten land filled with mysteries and wonders.",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 5 ,
                    ImageUrl = string.Empty
                },
                new Product
                {
                    Id = 5,
                    Title = "Journey to the Stars",
                    Author = "Ron Parker",
                    Description = "An inspiring voyage into space, following the courage of pioneers reaching for the unknown.",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3 ,
                    ImageUrl = string.Empty

                },
                new Product
                {
                    Id = 6,
                    Title = "Voices in the Rain",
                    Author = "Laura Phantom",
                    Description = "A touching drama where every drop of rain carries a memory of love, loss, and hope.",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 1 ,
                    ImageUrl = string.Empty
                }
            );
        }
    }
}
