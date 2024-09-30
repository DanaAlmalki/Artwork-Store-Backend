using System.Text;
using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Services.artist;
using Backend_Teamwork.src.Services.artwork;
using Backend_Teamwork.src.Services.category;
using Backend_Teamwork.src.Services.customer;
using Backend_Teamwork.src.Services.user;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using static Backend_Teamwork.src.Entities.User;

var builder = WebApplication.CreateBuilder(args);

// connect to database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);
dataSourceBuilder.MapEnum<UserRole>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
});

// add auto-mapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// add DI services
builder.Services.AddScoped<ICategoryService, CategoryService>().AddScoped<CategoryRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>().AddScoped<CustomerRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>().AddScoped<ArtistRepository>();
builder.Services.AddScoped<IArtworkService, ArtworkService>().AddScoped<ArtworkRepository>();
builder.Services.AddScoped<IUserService, UserService>().AddScoped<UserRepository>();

// add logic for authentication
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

// Athorization for Admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// add controllers
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
