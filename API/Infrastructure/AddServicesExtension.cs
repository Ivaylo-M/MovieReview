namespace API.Infrastructure
{
    using Application.Users;
    using Persistence.Repositories;

    public static class AddServicesExtension
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginUser).Assembly));

            return services;
        }
    }
}
