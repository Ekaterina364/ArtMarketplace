using ArtMarketplace.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string for EmailSenderMicroserviceDbContext is not configured.");
        }

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("ArtMarketplace.Infrastructure.EntityFramework");
            }));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Marketplace API",
                Description = "API for digital marketplace (artists, products, purchases, downloads)."
            });
        });


        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
