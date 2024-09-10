﻿namespace FileProcessingApp.Models.Entities.Base
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
