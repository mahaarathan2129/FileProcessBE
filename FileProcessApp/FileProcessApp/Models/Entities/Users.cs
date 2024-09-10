using FileProcessingApp.Models.Entities.Base;

namespace FileProcessingApp.Models.Entities
{
    public class Users : IBaseEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public ICollection<Files> Files { get; set; }
    }
}
