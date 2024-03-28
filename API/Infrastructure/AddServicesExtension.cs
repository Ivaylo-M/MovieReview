namespace API.Infrastructure
{
    using Persistence.Repositories;
    using Application.Users;
    using Application.Services.Contracts;
    using Application.Services;

    public static class AddServicesExtension
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginUser).Assembly));

            services.AddMemoryCache();

            return services;
        }
    }
}
