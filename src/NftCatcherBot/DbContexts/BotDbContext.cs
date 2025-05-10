using Microsoft.EntityFrameworkCore;
using NftCatcherApi.Entities;
using NftCatcherApi.Enums;

namespace NftCatcherApi.DbContexts;

public class BotDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BotUser> BotUsers { get; init; }
    public DbSet<NftSnipe> NftSnipes { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BotUser>().ToTable("BotUsers");
        modelBuilder.Entity<NftSnipe>().ToTable("NftSnipes");
        modelBuilder.Entity<BotUser>().HasData(
            new BotUser()
            {
                Id = 1, UserId = 6177562485, FullName = "Abudrrohman", Username = "mdvcc", Role = BotUserRole.Admin, 
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), IsDeleted = false
            });
    }
}