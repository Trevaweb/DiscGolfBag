using Microsoft.EntityFrameworkCore;

class DiscDb : DbContext
{
    public DiscDb(DbContextOptions<DiscDb> options)
        : base(options) { }

    public DbSet<Disc> Discs => Set<Disc>();
}