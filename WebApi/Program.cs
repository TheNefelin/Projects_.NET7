using Core.Data;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Application.Services;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;
using ProjectGamesGuide.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDapperContext>(provider =>
{
    return new DapperContext(builder.Configuration.GetConnectionString("SqlServerWeb")!);
});

builder.Services.AddTransient<IRepositoryByUser<AdventureUser>, AdventureUserRepository>();
builder.Services.AddTransient<IRepositoryByUser<GuideUser>, GuideUserRepository>();

builder.Services.AddTransient<IRepositoryBase<AdventureImg>, AdventureImgRepository>();
builder.Services.AddTransient<IRepositoryBase<Adventure>, AdventureRepository>();
builder.Services.AddTransient<IRepositoryBase<BackgroundImg>, BackgroundImgRepository>();
builder.Services.AddTransient<IRepositoryBase<Character>, CharacterRepository>();
builder.Services.AddTransient<IRepositoryBase<Game>, GameRepository>();
builder.Services.AddTransient<IRepositoryBase<Guide>, GuideRepository>();
builder.Services.AddTransient<IRepositoryBase<Source>, SourceRepository>();

builder.Services.AddTransient<IServiceByUser<AdventureUser>, AdventureUserService>();
builder.Services.AddTransient<IServiceByUser<GuideUser>, GuideUserService>();

builder.Services.AddTransient<IServiceBase<AdventureImg>, AdventureImgService>();
builder.Services.AddTransient<IServiceBase<Adventure>, AdventureService>();
builder.Services.AddTransient<IServiceBase<BackgroundImg>, BackgroundImgService>();
builder.Services.AddTransient<IServiceBase<Character>, CharacterService>();
builder.Services.AddTransient<IServiceBase<Game>,  GameService>();
builder.Services.AddTransient<IServiceBase<Guide>, GuideService>();
builder.Services.AddTransient<IServiceBase<Source>, SourceService>();

builder.Services.AddTransient<IGameGuideService, GameGuideService>();

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
