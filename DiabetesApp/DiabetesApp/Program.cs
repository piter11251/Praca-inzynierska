using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.Account;
using DiabetesApp.Dto.BloodPressureDtos;
using DiabetesApp.Dto.EntryDtos;
using DiabetesApp.Dto.Validators.Account;
using DiabetesApp.Dto.Validators.BloodPressure;
using DiabetesApp.Dto.Validators.Entry;
using DiabetesApp.Entities;
using DiabetesApp.Middleware;
using DiabetesApp.Services;
using DiabetesApp.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
    

// konfiguracja jwt
var key = Encoding.UTF8.GetBytes("SuperSecretKeyDon'tShareIt1234567890");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "diabetesapp.com",
            ValidAudience = "diabetesapp.com",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// dbcontext i serwisy
builder.Services.AddDbContext<DiabetesDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<DiabetesDbContext>();

/*builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DiabetesDbContext>()
    .AddDefaultTokenProviders();*/

builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<IBloodPressureService, BloodPressureService>();
builder.Services.AddScoped<IAccountService, AccountService>();

// walidatory

builder.Services.AddScoped<IValidator<CreateEntryDto>, EntryDtoValidator<CreateEntryDto>>();
builder.Services.AddScoped<IValidator<ModifyEntryDto>, EntryDtoValidator<ModifyEntryDto>>();
builder.Services.AddScoped<IValidator<CreateBloodPressureEntryDto>, CreateBloodPressureDtoValidator>();
builder.Services.AddScoped<IValidator<ModifyBloodPressureDto>, ModifyBloodPressureDtoValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
// middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<User>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(builder => 
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();

app.Run();
