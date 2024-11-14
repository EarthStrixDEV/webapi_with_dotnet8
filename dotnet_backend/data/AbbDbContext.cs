using Microsoft.EntityFrameworkCore;
using model;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Users> Users { get; set; }
}