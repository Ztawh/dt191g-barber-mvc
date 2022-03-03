using barber_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace barber_mvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customer => Set<Customer>();
    public DbSet<Barber> Barber => Set<Barber>();
    public DbSet<Appointment> Appointment => Set<Appointment>();
    public DbSet<Service> Service => Set<Service>();
}