using System.ComponentModel.DataAnnotations;

namespace TryAspNetCore.Api.Domain
{
    public class EventDto
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
    }
}