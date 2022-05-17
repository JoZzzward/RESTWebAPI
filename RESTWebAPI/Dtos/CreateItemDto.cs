using System;
using System.ComponentModel.DataAnnotations;

namespace RESTWebAPI.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 250)]
        public decimal Price { get; init; }
    }
}
