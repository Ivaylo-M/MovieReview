using API.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigurateDbContext(builder.Configuration);
builder.Services.ConfigurateIdentity(builder.Configuration);
builder.Services.ConfigurateCloudinary(builder.Configuration);
builder.Services.ConfigurateServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
