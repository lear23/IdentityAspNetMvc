using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Dtos;

public class CoursesDto
{
  
    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? ImageName { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public int Hours { get; set; }

    public bool IsBestSeller { get; set; }

    public decimal LikesNumbers { get; set; }

    public decimal LikesProcent { get; set; }


}
