using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(20)]
    public string Genre { get; set; } = default!;

    [Range(1, 100)]
    public decimal Price { get; set; } = 5M;

    public DateOnly ReleaseDate { get; set; }
}
