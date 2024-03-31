namespace OpenFlightsRazor.Models;

public class CustomSettings
{
    public bool UseLocalData { get; set; }
    public string? AirportsLocalPath { get; set; }
    public string? AirportsUri { get; set; }
    public string? RoutesLocalPath { get; set; }
    public string? RoutesUri { get; set; }

}
