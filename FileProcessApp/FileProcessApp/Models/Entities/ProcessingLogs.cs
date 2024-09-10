using FileProcessingApp.Models.Entities.Base;

namespace FileProcessingApp.Models.Entities
{
    public class ProcessingLogs : IBaseEntity
    {
        public long Id { get; set; }
        public long FileId { get; set; }
        public int LastProcessedOffset { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int RetryCount { get; set; }
        public DateTime? LastRetryAt { get; set; }
        public Files File { get; set; }
    }
}
