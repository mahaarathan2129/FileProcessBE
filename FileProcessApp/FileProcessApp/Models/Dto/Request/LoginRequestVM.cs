namespace FileProcessingApp.Models.Dto.Request
{
    public record LoginRequestVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
