using Microsoft.Extensions.DependencyInjection;

namespace Medixa_AI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Application Services (to be implemented)
            // services.AddScoped<IPatientService, PatientService>();
            // services.AddScoped<IDoctorService, DoctorService>();
            // services.AddScoped<IOrderService, OrderService>();
            // services.AddScoped<IResultService, ResultService>();
            // services.AddScoped<IAIService, AIService>();

            return services;
        }
    }
}
