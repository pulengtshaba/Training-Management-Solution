using Microsoft.EntityFrameworkCore;
using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<User> Users { get; set; }
}