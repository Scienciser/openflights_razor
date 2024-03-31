using Microsoft.AspNetCore.Mvc;
using OpenFlightsRazor.Models;
using OpenFlightsRazor.Services;
using Route = OpenFlightsRazor.Models.Route;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<CustomSettings>(
    builder.Configuration.GetSection("CustomSettings")
);

builder.Services.AddSingleton<DataService>();
builder.Services.AddScoped<MappingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/GetDirectFlightsByName/{sourceAirportName}/{destAirportName}",
    async (
        string sourceAirportName,
        string destAirportName,
        [FromServices] MappingService svc
    ) =>
    { 
        try
        {
            IList<Route> res = await svc.GetDirectFlightsByName(sourceAirportName, destAirportName);
            return Results.Ok(res);
        }
        catch (OFRInvalidInputException e) 
        {
            return Results.BadRequest(e.Message);
        }
        // OFRLogicExceptions not caught here and so create a 500 Internal Server Error.
    })
    .Produces<IList<Route>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);


app.MapGet("/GetDirectFlightsByCode/{sourceAirportCode}/{destAirportCode}",
    async (
        string sourceAirportCode,
        string destAirportCode,
        [FromServices] MappingService svc
    ) =>
    { 
        try
        {
            IList<Route> res = await svc.GetDirectFlightsByCode(sourceAirportCode, destAirportCode);
            return Results.Ok(res);
        }
        catch (OFRInvalidInputException e) 
        {
            return Results.BadRequest(e.Message);
        }
        // OFRLogicExceptions not caught here and so create a 500 Internal Server Error.
    })
    .Produces<IList<Route>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);


app.Run();
