using CollegeApp.DatabaseContext;
using CollegeApp.MyLogging;
using Serilog;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Configurations;
using CollegeApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CollegeApp.Service;

var builder = WebApplication.CreateBuilder(args);
var keyforlocal = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));
var keyforDemo = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretDemo"));
var LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");
var LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");
var DemoAudience = builder.Configuration.GetValue<string>("DemoAudience");
var DemoIssuer = builder.Configuration.GetValue<string>("DemoIssuer");
// Add services to the container.
//builder.Logging.ClearProviders();
//builder.Logging.AddDebug();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson();
builder.Services.AddDbContext<DatabContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnDB"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen( options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization using Bearer scheme. Enter Bearer [space] add token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id= "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddAutoMapper(typeof(AutoMapperconfig));
builder.Services.AddSingleton<IMyLogger, LogToFile>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ICacheProvider, CacheProvider>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                         // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
    options.AddPolicy("LocalHost",
                      policy =>
                      {
                         // policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                           policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
    options.AddPolicy("DemoOrigin",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                          // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("LocalJwt",options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyforlocal),
        ValidateIssuer = true,
        ValidIssuer = LocalIssuer,
        ValidateAudience = true,
        ValidAudience = LocalAudience,

    };

}).AddJwtBearer("DemoJwt", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyforDemo),
        ValidateIssuer = true,
        ValidIssuer = DemoIssuer,
        ValidateAudience = true,
        ValidAudience = DemoAudience,

    };

});
//builder.WebHost.UseUrls("http://*:6001");
var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
