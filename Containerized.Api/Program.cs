using Containerized.Api.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
Configure(app, db);

static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    // Add database connection to the container.
    services.AddDbContext<ApplicationDbContext>(opt =>
        opt.UseNpgsql(config.GetConnectionString("DefaultConnection")
    ));

    // Add services to the container.
    services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

static void Configure(WebApplication app, ApplicationDbContext db)
{
    db.Database.Migrate();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
