using Microsoft.EntityFrameworkCore;
using UserApi;
using UserApi.Endpoints;
using UserApi.Repositories;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection"));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar ILogger
builder.Services.AddLogging();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapGroup("/users").MapUsers();

app.Run();
