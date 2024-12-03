using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto;
using DiabetesApp.Dto.Validators;
using DiabetesApp.Middleware;
using DiabetesApp.Services;
using DiabetesApp.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DiabetesDbContext>();
builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<IBloodPressureService, BloodPressureService>();
builder.Services.AddScoped<IValidator<CreateEntryDto>, EntryDtoValidator<CreateEntryDto>>();
builder.Services.AddScoped<IValidator<ModifyEntryDto>, EntryDtoValidator<ModifyEntryDto>>();
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
