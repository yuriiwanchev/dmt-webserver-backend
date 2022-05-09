using DMT.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DMT.DataAccess;

internal class UsersDbContext : IdentityDbContext<IdentityUser>
{
    // public DbSet<UserDb> UserDbs { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}