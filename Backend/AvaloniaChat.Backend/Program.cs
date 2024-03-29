
using AvaloniaChat.Application.Configs;
using AvaloniaChat.Backend.Business.Repository.Implimentations;
using AvaloniaChat.Backend.Business.Repository.Interfaces;
using AvaloniaChat.Backend.Business.Services.Implimentations;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Backend.Hubs;
using AvaloniaChat.Backend.Middleware;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Business.Mapping;
using AvaloniaChat.Business.Repository.Implimentations;
using AvaloniaChat.Business.Repository.Interfaces;
using AvaloniaChat.Business.Services.Implimentations;
using AvaloniaChat.Data;
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using AvaloniaChat.Infrastructure.Services;
using AvaloniaChat.Infrastructure.Services.Implimentations;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("PostgreSQL");
if (connection.IsNullOrEmpty())
{
    throw new InvalidOperationException("Connection string not found");
}
builder.Services.AddDbContext<ChatDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connection));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors();

// repository
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

// services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IMessageService, MessageService>();


var getJwtSection = builder.Configuration.GetSection(JwtConfig.Position);
var jwtConfig = getJwtSection.Get<JwtConfig>();
builder.Services.Configure<JwtConfig>(getJwtSection);

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
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = jwtConfig.SymmetricSecurityKey()
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/chatHub")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3001", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
app.Run();

