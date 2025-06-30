using Microsoft.EntityFrameworkCore;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Common.Security;
using Tourism.Api.Interfaces;
using Tourism.Api.Services;

// Services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

//database
string connectionString = builder.Configuration.GetConnectionString("database")!;
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


// Middleware
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
