using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _accessor;
    public ApplicationDbContextSeed(ApplicationDbContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }
    
    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsMySql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            // Logging.Error("An error occurred while initialising the database.");
            throw;
        }
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.CommitAsync();
        }
        catch (Exception e)
        {
            // Logging.Error("An error occurred while seeding the database;");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Weights.Any())
        {
            var weightCategories = new List<Weight>
            {
                new Weight { Code = "G", Description = "Gam" },
                new Weight { Code = "KG", Description = "Kilogram" },
            };

            _context.Weights.AddRange(weightCategories);
            await _context.SaveChangesAsync();
        }
    }
}