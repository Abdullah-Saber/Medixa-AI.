using Microsoft.EntityFrameworkCore;

namespace Medixa_AI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Add DbSets for your entities here
        // Example:
        // public DbSet<Patient> Patients { get; set; }
        // public DbSet<Doctor> Doctors { get; set; }
        // public DbSet<Appointment> Appointments { get; set; }
        // etc.
    }
}
