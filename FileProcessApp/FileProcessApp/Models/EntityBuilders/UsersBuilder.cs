using FileProcessingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FileProcessingApp.Models.EntityBuilders
{
    public class UsersBuilder : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder
            .HasKey(u => u.Id);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder
                .Property(u => u.PasswordHash)
                .IsRequired();
            builder
                .Property(u => u.FirstName)
                .HasMaxLength(100);
            builder
                .Property(u => u.LastName)
                .HasMaxLength(100);
        }
    }
}
