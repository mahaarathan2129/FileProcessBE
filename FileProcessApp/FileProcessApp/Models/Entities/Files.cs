using FileProcessingApp.Models.Entities.Base;

namespace FileProcessingApp.Models.Entities
{
    public class Files : IBaseEntity, IAuditableEntity
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Status { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? LastProcessedAt { get; set; }
        public TimeSpan? ProcessingDuration { get; set; }
        public string ProcessingDetails { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long UserId { get; set; }
        public Users User { get; set; }
        public ICollection<ProcessingLogs> ProcessingLog { get; set; }
    }
}
