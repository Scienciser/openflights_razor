# OpenFlights Search Tool

This is an ASP.NET application that allows searching OpenFlights airline route data.

It is composed of two elements:
1. An ASP.NET Web API, with Swagger documentation.
2. A Razor page that allows the data to be queried directly.

## Configure

Modify the `CustomSettings` section in the provided `appsettings.json` file to change the resource paths. The `UseLocalData` boolean determines whether the OpenFlights data is remotely retrieved or populated from the `data` folder. 

## Run

```dotnet run```

The Razor page can be found at http://localhost:5239 and the Swagger API documentation at http://localhost:5239/swagger

