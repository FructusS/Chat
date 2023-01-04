using AvaloniaChat.Backend.Configs;
using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Hubs;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

//jwt config
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
       
        ValidateIssuer = true,
     
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
 
        ValidateAudience = true,
      
        ValidAudience = builder.Configuration["Jwt:Audience"],
     
        ValidateLifetime = true,
     
        IssuerSigningKey = new
            SymmetricSecurityKey
            (Encoding.UTF8.GetBytes
            (builder.Configuration["Jwt:Key"])),
      
        ValidateIssuerSigningKey = true
    };
});
// database context
//signalr



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
app.MapHub<ChatHub>("/chatHub");
app.Run();
