using Airbnb.Api.Infrastructure;
using Airbnb.Api.Infrastructure.Middleware;
using Airbnb.Api.Repository;
using Airbnb.Api.Services;
using Airbnb.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Db config
var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection");
builder.Services.AddDbContext<AirbnbDbContext>((options) =>
{
    options.UseSqlServer(connectionString);
});
//Dependency Injection
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper((ops) =>
{
    ops.AddProfile(new MappingProfile());
}, Assembly.GetExecutingAssembly());

builder.Services.AddOptions<JwtSettings>()
                .BindConfiguration($"{nameof(JwtSettings)}")
                .ValidateDataAnnotations()
                .ValidateOnStart();

builder.Services.AddScoped<ExceptionMiddleware>();

//JWT token config
builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

builder.Services
    .AddAuthentication(authentication =>
    {
        authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, null!);
//logging
builder.Services.AddOptions<LoggerSettings>().BindConfiguration(nameof(LoggerSettings));

_ = builder.Host.UseSerilog((_, sp, serilogConfig) =>
{
    var loggerSettings = sp.GetRequiredService<IOptions<LoggerSettings>>().Value;
    string appName = loggerSettings.AppName;
    bool writeToFile = loggerSettings.WriteToFile;
    bool structuredConsoleLogging = loggerSettings.StructuredConsoleLogging;
    string minLogLevel = loggerSettings.MinimumLogLevel;
    serilogConfig.WriteTo.File(
                 new CompactJsonFormatter(),
                 "Logs/logs.json",
                 restrictedToMinimumLevel: LogEventLevel.Information,
                 rollingInterval: RollingInterval.Day,
                 retainedFileCountLimit: 365);
    Console.WriteLine(loggerSettings.AppName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
