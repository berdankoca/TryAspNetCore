using System.ComponentModel.DataAnnotations;

namespace TryAspNetCore.Api.Domain
{
    public class RegisterEventDto
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
    }
}