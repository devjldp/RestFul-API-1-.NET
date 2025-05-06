using Employees.Data;
using Microsoft.EntityFrameworkCore;

// Create the WebApplicationBuilder instance that sets up the app's configuration, services, and environment
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add the service for the context.
builder.Services.AddDbContext<AppDbContext>( options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB")));

// Add the sercvice for controllers.
builder.Services.AddControllers();

// Build the application from the configured builder
// The 'app' instance will be used to define middleware, routes, controllers, etc.
var app = builder.Build();

// Optional, forces HTTPS
app.UseHttpsRedirection();

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
