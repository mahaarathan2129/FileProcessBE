using FileProcessingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FileProcessingApp.Models.EntityBuilders
{
    public class FilesBuilder : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            builder.ToTable("Files");
            builder
            .HasKey(f => f.Id);
            builder
                .Property(f => f.FilePath)
                .IsRequired();
            builder
                .Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);
            builder
                .Property(f => f.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder
            .HasOne(f => f.User)
            .WithMany(u => u.Files)
            .HasForeignKey(f => f.UserId);
        }
    }
}
