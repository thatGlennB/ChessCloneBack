using BLL = ChessCloneBack.BLL;
using ChessCloneBack.DAL;
using ChessCloneBack.DAL.Interfaces;
using ChessCloneBack.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

#region Dependency Injection
    builder.Services.AddScoped<BLL.Interfaces.IAuthenticationService, BLL.AuthenticationService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
