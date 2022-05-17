using System.ComponentModel.DataAnnotations;

namespace RESTWebAPI.Dtos
{
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 200)]
        public int Price{ get; init; }
    }
}
