using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Services.category;
using Backend_Teamwork.src.Utils;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
});
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddScoped<ICategoryService, CategoryService>().AddScoped<CategoryRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
