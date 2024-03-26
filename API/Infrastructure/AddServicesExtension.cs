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
            services.AddScoped<IPhotoService, PhotoService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginUser).Assembly));

            return services;
        }
    }
}
