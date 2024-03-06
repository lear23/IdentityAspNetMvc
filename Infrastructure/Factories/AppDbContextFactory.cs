using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Factories;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SkolaUppgifter\ASP.NET\IdentityAspNetMvc\Infrastructure\Data\Local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        return new AppDbContext(optionsBuilder.Options);
    }
}
