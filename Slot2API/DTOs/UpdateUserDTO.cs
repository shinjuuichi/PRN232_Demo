using System.ComponentModel.DataAnnotations;

namespace Slot2API.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
