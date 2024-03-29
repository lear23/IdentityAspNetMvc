using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CourseController(Datacontext context) : ControllerBase
{

    private readonly Datacontext _context = context;


    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CoursesDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Courses.AnyAsync(x => x.Title == dto.Title))
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
    #endregion


    #region GET
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
    #endregion


    #region UPDATE

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CoursesDto dto)
    {
        if (ModelState.IsValid)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                course.Title = dto.Title;
                course.Author = dto.Author;
                course.IsBestSeller = dto.IsBestSeller;
                course.Discount = dto.Discount;
                course.Price = dto.Price;
                course.Hours = dto.Hours;
                course.LikesNumbers = dto.LikesNumbers;
                course.LikesProcent = dto.LikesProcent;
                course.ImageName = dto.ImageName;

                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return NotFound();
        }
        return BadRequest();
    }

    #endregion



    #region Delete
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(string id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
    #endregion

}
