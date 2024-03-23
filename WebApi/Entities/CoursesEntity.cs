using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class CoursesEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? ImageName { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    public int Hours { get; set; }

    public bool IsBestSeller { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LikesNumbers { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LikesProcent { get; set; }

}
