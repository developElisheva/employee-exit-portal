using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - React DEV
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Application Services (DI)
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ExitFormService>();
builder.Services.AddScoped<ExitTaskService>();

#endregion

var app = builder.Build();

#region Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS חייב להיות לפני Authorization ו-MapControllers
app.UseCors("AllowReactDev");

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
