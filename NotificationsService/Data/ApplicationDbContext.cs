using Microsoft.EntityFrameworkCore;
using NotificationsService.Models;

namespace NotificationsService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
}