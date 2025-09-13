using Core.Data;
using ProjectAuth.Application.Interfaces;
using ProjectAuth.Application.Services;
using ProjectAuth.Domain.Interfaces;
using ProjectAuth.Infrastructure.Repositories;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Application.Services;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;
using ProjectGamesGuide.Infrastructure.Repositories;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Application.Services;
using ProjectPasswordManager.Domain.Interfaces;
using ProjectPasswordManager.Infrastructure.Repositories;
using Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDapperContext>(provider =>
{
    return new DapperContext(builder.Configuration.GetConnectionString("SqlServerWeb")!);
});

// ======================================================================
// Core Repository and Services
// ======================================================================
builder.Services.AddTransient<ICoreDataRepository, CoreDataRepository>();
builder.Services.AddTransient<ICoreUserRepository, CoreUserRepository>();
builder.Services.AddTransient<ICoreDataService, CoreDataService>();
builder.Services.AddTransient<ICoreUserService, CoreUserService>();
builder.Services.AddSingleton<PasswordUtil>();

// ======================================================================
// Auth Repository and Services
// ======================================================================
builder.Services.AddTransient<IAuthUserRepository, AuthUserRepository>();
builder.Services.AddTransient<IAuthUserService, AuthUserService>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
app.UseAuthorization();
app.MapControllers();
app.Run();
