using System.ComponentModel.DataAnnotations;

namespace TasksApi.DTOs
{
    public class RegisterDTO
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FullName { get; set; }
    }
}
