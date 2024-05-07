using BLL = ChessCloneBack.BLL;
using ChessCloneBack.DAL;
using ChessCloneBack.DAL.Interfaces;
using ChessCloneBack.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.BLL;
using ChessCloneBack.Templates.Interfaces;
using ChessCloneBack.Templates.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region JWT Authentication
if (builder.Configuration["Jwt:Key"] == null)
    throw new NullReferenceException("Null configuration value: no Jwt Key value set.");
builder.Services.AddAuthentication()
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});
#endregion

#region DbConfiguration
    builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.
    GetConnectionString("Database")));

    builder.Services.AddScoped<DatabaseContext>();
#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region Dependency Injection
    builder.Services.AddScoped<BLL.Interfaces.IAuthenticationService, BLL.AuthenticationService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
    name: "AppOrigins",
    policy =>
    {
        policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AppOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
