namespace API.Infrastructure
{
    using CloudinaryDotNet;

    public static class CloudinaryServiceExtensions
    {
        public static void ConfigurateCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                Account account = new Account(
                    configuration["CloudinarySettings:CloudName"],
                    configuration["CloudinarySettings:ApiKey"],
                    configuration["CloudinarySettings:ApiSecret"]);

                return new Cloudinary(account);
            });
        }
    }
}
