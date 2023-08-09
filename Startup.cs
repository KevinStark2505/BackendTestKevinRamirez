using HotelReservas.API.Repositories;
using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Repositories;
using HotelReservas.Domain.Repositories.Interfaces;
using HotelReservas.Domain.Services;
using HotelReservas.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Configurar Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Reservas", Version = "v1" });
        });

        services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

        // Registro del cliente de MongoDB
        services.AddSingleton<IMongoClient>(sp => new MongoClient(Configuration.GetConnectionString("MongoDb")));

        // Registro de la base de datos de MongoDB
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddTransient<IAdministracionHotelService, AdministracionHotelService>();
        services.AddTransient<IReservaService, ReservaService>();
        services.AddTransient<IAdministracionHotelRepository, AdministracionHotelRepository>();
        services.AddTransient<IReservaRepository, ReservaRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseSwagger();

        if (Configuration.GetValue<bool>("RunAtServer") == true)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/" + Configuration.GetValue<string>("NameAPI") + "/swagger/v1/swagger.json", Configuration.GetValue<string>("NameAPI"));
            });
        }
        else
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration.GetValue<string>("NameAPI"));
            });
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
