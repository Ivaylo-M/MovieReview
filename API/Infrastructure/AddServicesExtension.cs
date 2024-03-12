namespace API.Infrastructure
{
    using Persistence.Repositories;
    using Application.Users;

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
