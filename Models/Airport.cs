using CsvHelper.Configuration.Attributes;

namespace OpenFlightsRazor.Models;

public class Airport
{
    [Index(0)]
    public int Id { get; set; }
    
    [Index(1)]
    public string? Name { get; set; }
    
    [Index(2)]
    public string? City { get; set; }
    
    [Index(3)]
    public string? Country { get; set; }
    
    [Index(4)]
    public string? Iata { get; set; }
    
    [Index(5)]
    public string? Icao { get; set; }
    
    [Index(6)]
    public string? Latitude { get; set; }
    
    [Index(7)]
    public string? Longitude { get; set; }
    
    [Index(8)]
    public string? Altitude { get; set; }
    
    [Index(9)]
    public string? Timezone { get; set; }
    
    [Index(10)]
    public string? Dst { get; set; }
    
    [Index(11)]
    public string? Tz { get; set; }
    
    [Index(12)]
    public string? Type { get; set; }
    
    [Index(13)]
    public string? Source { get; set; }
}