using Employees.Data;
using Microsoft.EntityFrameworkCore;

// namespaces needed for authentication.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Create the WebApplicationBuilder instance that sets up the app's configuration, services, and environment
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add the service for the context.
builder.Services.AddDbContext<AppDbContext>( options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB")));

// Add the sercvice for controllers.
builder.Services.AddControllers();


// Configure JWT from appsettings
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = jwtConfig["Key"];
var issuer = jwtConfig["Issuer"];
var audience = jwtConfig["Audience"];




// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validate the token issuer
        ValidateAudience = true, // Validate the token audience
        ValidateLifetime = true, // Validate expiration
        ValidateIssuerSigningKey = true, // Validate the signature key
        ValidIssuer = issuer, // Replace with your issuer
        ValidAudience = audience, // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // Replace with your secure key
        };
    });

// Add authorization service
builder.Services.AddAuthorization();

// Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://127.0.0.1:5500")   // or "http://localhost:5500"
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



// Build the application from the configured builder
// The 'app' instance will be used to define middleware, routes, controllers, etc.
var app = builder.Build();

// Optional, forces HTTPS
app.UseHttpsRedirection();
// Add cors
app.UseCors("AllowFrontend");

// Enable authentication
app.UseAuthentication();

// Enables authorization (necessary if you add [Authorize] later)
app.UseAuthorization();

// Maps all controller routes (using [Route] and [HttpXxx] attributes)
app.MapControllers();


app.Run();


// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
