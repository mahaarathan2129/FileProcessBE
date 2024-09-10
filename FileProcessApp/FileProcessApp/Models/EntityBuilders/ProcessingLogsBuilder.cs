using FileProcessingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FileProcessingApp.Models.EntityBuilders
{
    public class ProcessingLogsBuilder : IEntityTypeConfiguration<ProcessingLogs>
    {
        public void Configure(EntityTypeBuilder<ProcessingLogs> builder)
        {
            builder.ToTable("ProcessingLogs");

            builder
            .HasKey(pl => pl.Id);

            builder
                .HasOne(pl => pl.File)
                .WithMany(f => f.ProcessingLog)
                .HasForeignKey(pl => pl.FileId);
        }
    }
}
