using Airbnb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = Environment.GetEnvironmentVariable("Environment");
        if (env == "Development" || env == null)
        {
            config.AddJsonFile($"appsettings.Development.json", optional: true);
        }
        else
        {
            config.AddJsonFile("appsettings.json", optional: true);
        }
    })
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration["ConnectionStrings:ApplicationContextConnection"];
        services.AddDbContext<AirbnbDbContext>(options =>
            options.UseSqlServer(connectionString, b=>b.MigrationsAssembly("Airbnb.Migrator")));
    });


var app = builder.Build();

//Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AirbnbDbContext>();
    dbContext.Database.Migrate();
    Console.WriteLine("---------------------------------Running Migration successfully.--------------------------------------");
}

try
{
    // Your app initialization and setup code here

    app.Run();
    return 0; // No error, successful execution
}
catch (Exception ex)
{
    Console.Write(ex);
    return 1; // Error occurred
}

