using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.DisplayOrder)
                   .IsRequired();
            builder.HasKey(c => c.Id);
            builder.ToTable("Categories");
            builder.Property(builder => builder.Id).ValueGeneratedOnAdd();
            builder.HasData(
                new Category { Id = 1 ,  Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2 , Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3 ,Name = "History", DisplayOrder = 3 }
            );
        

        }
    }
}
