using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MilsatIMS.Data;
using MilsatIMS.Interfaces;
using MilsatIMS.Repositories;
using MilsatIMS.Services;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
    
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<MilsatIMSContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MilsatIMSContext"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MilsatIMSContext"))));
    //builder.Services.AddDbContext<MilsatIMSContext>(options =>
    //options.UseSqlServer(
    //    builder.Configuration.GetConnectionString("MilsatIMSContext") ?? throw new InvalidOperationException("Connection string 'MilsatIMSContext' not found.")));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    builder.Services.AddHttpContextAccessor();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header uing the Bearer scheme (\"Bearer {token} \")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
    builder.Services.AddScoped<IAuthentication, AuthService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IInternService, InternService>();
    builder.Services.AddScoped<IMentorService, MentorService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IStatsService, StatsService>();
    builder.Services.AddScoped<IPromptService, PromptService>();
    builder.Services.AddScoped<IActivityService, ActivityService>();

    builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
        policy =>
        {
            policy.WithOrigins(
                builder.Configuration.GetSection("OriginBase1").Value,
                builder.Configuration.GetSection("OriginBase2").Value)
                .AllowAnyMethod().AllowAnyHeader();
        }));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI(); 
    //}
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("NgOrigins");
    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
