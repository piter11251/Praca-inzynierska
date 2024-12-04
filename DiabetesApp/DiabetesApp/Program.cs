using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.BloodPressureDtos;
using DiabetesApp.Dto.EntryDtos;
using DiabetesApp.Dto.Validators.BloodPressure;
using DiabetesApp.Dto.Validators.Entry;
using DiabetesApp.Entities;
using DiabetesApp.Middleware;
using DiabetesApp.Services;
using DiabetesApp.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dbcontext i serwisy
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DiabetesDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<DiabetesDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<IBloodPressureService, BloodPressureService>();

// walidatory

builder.Services.AddScoped<IValidator<CreateEntryDto>, EntryDtoValidator<CreateEntryDto>>();
builder.Services.AddScoped<IValidator<ModifyEntryDto>, EntryDtoValidator<ModifyEntryDto>>();
builder.Services.AddScoped<IValidator<CreateBloodPressureEntryDto>, CreateBloodPressureDtoValidator>();
builder.Services.AddScoped<IValidator<ModifyBloodPressureDto>, ModifyBloodPressureDtoValidator>();

// middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
