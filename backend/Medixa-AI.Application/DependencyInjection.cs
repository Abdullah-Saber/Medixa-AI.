using Medixa_AI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medixa_AI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Application Services (to be implemented)
            // services.AddScoped<IPatientService, PatientService>();
            // services.AddScoped<IDoctorService, DoctorService>();
            // etc.

            return services;
        }
    }
}
