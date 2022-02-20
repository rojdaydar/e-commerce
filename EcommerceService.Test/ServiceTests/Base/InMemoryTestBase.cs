using System;
using EcommerceService.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.Test.ServiceTests.Base;

public class InMemoryTestBase : IDisposable
{
    protected readonly EcommerceDbContext _dbContext;

    public InMemoryTestBase()
    {
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _dbContext = new EcommerceDbContext(options);
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}