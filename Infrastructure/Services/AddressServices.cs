
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Services;

public class AddressServices(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<AddressEntity> GetAddressAsync(string UserId)
    {
        var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == UserId);
        return addressEntity!;
    }

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        _context.Addresses.Add(entity);
        await _context.SaveChangesAsync();
        return true;
    
    }
    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        var exists = await _context.Addresses.FirstOrDefaultAsync(x =>x.UserId == entity.UserId);
        if (exists != null)
        {
            _context.Entry(exists).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;   
    }

}
