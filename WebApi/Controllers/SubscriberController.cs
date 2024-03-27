using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Dtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriberController(Datacontext context) : ControllerBase
{
    private readonly Datacontext _context = context;


    #region

    [HttpPost]
    public async Task<IActionResult> Create(SubscriberDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == dto.Email))
            {
                try
                {
                    _context.Subscribers.Add(dto);
                    await _context.SaveChangesAsync();
                    return Created("", null);
                }
                catch
                {
                    return Problem("Unable to create subscription");
                }
            }
            return Conflict("This email address is already subscribed");
        }
        return BadRequest();
    }
    #endregion


    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscriber = await _context.Subscribers.ToListAsync();
        if (subscriber.Count != 0)
        {
            return Ok(subscriber);
        }

        return NotFound();
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetOne(string email)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);

        if (subscriber != null)
        {
            return Ok(subscriber);
        }
        return NotFound();
    }

    #endregion


    #region Update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, string email)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

        if (subscriber != null)
        {

            subscriber.Email = email;
            _context.Subscribers.Update(subscriber);
            await _context.SaveChangesAsync();

            return Ok(subscriber);
        }
        return NotFound();
    }

    #endregion


    #region DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

        if (subscriber != null)
        {

            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return Ok();
        }
        return NotFound();
    }

    #endregion


}
