global using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Tourism.Api.Common.Configurations;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Common.Security;
using Tourism.Api.Interfaces;
using Tourism.Api.Services;

// Services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.ConfigureAuth();
builder.Services.ConfigureSwaggerAuthorize();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();    
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAdminStatisticsService, AdminStatisticsService>();


//database
string connectionString = builder.Configuration.GetConnectionString("database")!;
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


// Middleware
var app = builder.Build();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
