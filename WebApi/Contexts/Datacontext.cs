using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Contexts;

public class Datacontext(DbContextOptions<Datacontext> options) : DbContext(options)
{
    public DbSet<CoursesEntity> Courses { get; set; }




}
