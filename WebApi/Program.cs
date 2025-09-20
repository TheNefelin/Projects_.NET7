using Core.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectAuth.Application.Interfaces;
using ProjectAuth.Application.Services;
using ProjectAuth.Domain.Interfaces;
using ProjectAuth.Infrastructure.Repositories;
using ProjectAuth.Infrastructure.Services;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Application.Services;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;
using ProjectGamesGuide.Infrastructure.Repositories;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Application.Services;
using ProjectPasswordManager.Domain.Interfaces;
using ProjectPasswordManager.Infrastructure.Repositories;
using ProjectPortfolio.Application.DTOs;
using ProjectPortfolio.Application.Interfaces;
using ProjectPortfolio.Application.Services;
using ProjectPortfolio.Domain.Entities;
using ProjectPortfolio.Domain.Interfaces;
using ProjectPortfolio.Infrastructure.Repositories;
using System.Text;
using Utils;
using WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDapperContext>(provider =>
{
    return new DapperContext(builder.Configuration.GetConnectionString("SqlServerWeb")!);
});

// ======================================================================
// Filters
// ======================================================================
builder.Services.AddSingleton<ApiKeyFilter>();

// ======================================================================
// Utils Services
// ======================================================================
builder.Services.AddSingleton<EncryptionUtil>();
builder.Services.AddSingleton<PasswordUtil>();

// ======================================================================
// Auth Repository and Services
// ======================================================================
builder.Services.AddTransient<IAuthUserRepository, AuthUserRepository>();
builder.Services.AddTransient<IMaeConfigRepository, MaeConfigRepository>();
builder.Services.AddTransient<IAuthUserService, AuthUserService>();
builder.Services.AddTransient<IMaeConfigService, MaeConfigService>();
builder.Services.AddSingleton<JwtTokenUtil>();

// ======================================================================
// Password Manager Repository and Services
// ======================================================================
builder.Services.AddTransient<ICoreDataRepository, CoreDataRepository>();
builder.Services.AddTransient<ICoreUserRepository, CoreUserRepository>();
builder.Services.AddTransient<ICoreDataService, CoreDataService>();
builder.Services.AddTransient<ICoreUserService, CoreUserService>();

// ======================================================================
// Game Guides Repository and Services
// ======================================================================
builder.Services.AddTransient<IAuthGoogleRepository, AuthGoogleRepository>();
builder.Services.AddTransient<IRepositoryByUser<GuideUser>, GuideUserRepository>();
builder.Services.AddTransient<IRepositoryByUser<AdventureUser>, AdventureUserRepository>();
builder.Services.AddTransient<IRepositoryBase<Game>, GameRepository>();
builder.Services.AddTransient<IRepositoryBase<Character>, CharacterRepository>();
builder.Services.AddTransient<IRepositoryBase<Source>, SourceRepository>();
builder.Services.AddTransient<IRepositoryBase<BackgroundImg>, BackgroundImgRepository>();
builder.Services.AddTransient<IRepositoryBase<Guide>, GuideRepository>();
builder.Services.AddTransient<IRepositoryBase<Adventure>, AdventureRepository>();
builder.Services.AddTransient<IRepositoryBase<AdventureImg>, AdventureImgRepository>();

builder.Services.AddTransient<IGameGuideService, GameGuideService>();
builder.Services.AddTransient<IAuthGoogleService, AuthGoogleService>();
builder.Services.AddTransient<IServiceByUser<GuideUser>, GuideUserService>();
builder.Services.AddTransient<IServiceByUser<AdventureUser>, AdventureUserService>();
builder.Services.AddTransient<IServiceBase<Game>, GameService>();
builder.Services.AddTransient<IServiceBase<Character>, CharacterService>();
builder.Services.AddTransient<IServiceBase<Source>, SourceService>();
builder.Services.AddTransient<IServiceBase<BackgroundImg>, BackgroundImgService>();
builder.Services.AddTransient<IServiceBase<Guide>, GuideService>();
builder.Services.AddTransient<IServiceBase<Adventure>, AdventureService>();
builder.Services.AddTransient<IServiceBase<AdventureImg>, AdventureImgService>();

// ======================================================================
// Porfolio Repository and Services
// ======================================================================
builder.Services.AddTransient<IRepositoryPortfolioBase<Project>, ProjectRepository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<Language>, LanguageRepository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<Technology>, TechnologyRepository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<Pro_Lang>, Pro_Lang_Repository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<Pro_Tech>, Pro_Tech_Repository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<UrlGrp>, UrlGrpRepository>();
builder.Services.AddTransient<IRepositoryPortfolioBase<Url>, UrlRepository>();

builder.Services.AddTransient<IServicePortfolioBase<Project>, ProjectService>();
builder.Services.AddTransient<IServicePortfolioBase<Language>, LanguageService>();
builder.Services.AddTransient<IServicePortfolioBase<Technology>, TechnologyService>();
builder.Services.AddTransient<IServicePortfolioBase<UrlGrp>, UrlGrpService>();
builder.Services.AddTransient<IServicePortfolioBase<Url>, UrlService>();

builder.Services.AddTransient<IServicePortfolioBase<ProjectResponse>, PublicProjectsService>();
builder.Services.AddTransient<IServicePortfolioBase<UrlResponse>, PublicUrlsService>();

// ======================================================================
// JWT Authentication Configuration
// ======================================================================
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// ======================================================================
// Add CORS Policy
// ======================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_allowedOrigins",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "http://127.0.0.1:4200" 
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ======================================================================
// Swagger Configuration with JWT
// ======================================================================
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Projects API",
        Version = "v1",
        Description = "API with JWT Authentication"
    });
    // Configuraci�n para JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    // Filtro personalizado para aplicar seguridad solo a endpoints con [Authorize]
    c.OperationFilter<AuthorizeOperationFilter>();
});

var app = builder.Build();

// ======================================================================
// Add Swagger UI
// ======================================================================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./swagger/v1/swagger.json", "Projects API v1");
    c.RoutePrefix = string.Empty;
    c.DisplayRequestDuration();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ======================================================================
// Cors Middleware
// ======================================================================
app.UseCors("_allowedOrigins");

// ======================================================================
// Authentication and Authorization Middleware
// ======================================================================
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.Run();
