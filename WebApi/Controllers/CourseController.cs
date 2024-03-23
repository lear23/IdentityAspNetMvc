using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController(Datacontext context) : ControllerBase
{

    private readonly Datacontext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(CoursesDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Courses.AnyAsync(x => x.Title == x.Title))
            {
                var courseEntity = new CoursesEntity
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Hours = dto.Hours,
                    Price = dto.Price,
                    LikesNumbers = dto.LikesNumbers,
                    LikesProcent = dto.LikesProcent,
                    ImageName = dto.ImageName,
                    IsBestSeller = dto.IsBestSeller,
                    Discount = dto.Discount

                };
                _context.Courses.Add(courseEntity);
                await _context.SaveChangesAsync();
                return Created("", null);
            }

            return Conflict();
        }
        return BadRequest();
    }


    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetOne(string id)
    {
        var courseEntity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (courseEntity != null)
        {
            return Ok(courseEntity);
        }
        return NotFound();          
    }

}
