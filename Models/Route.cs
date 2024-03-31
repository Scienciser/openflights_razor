using CsvHelper.Configuration.Attributes;

namespace OpenFlightsRazor.Models;

public class Route
{
    [Index(0)]
    public string? AirlineCode { get; set; }

    [Index(1)]
    public int? AirlineId { get; set; }

    [Index(2)]
    public string? SourceAirportCode { get; set; }

    [Index(3)]
    public int? SourceAirportId { get; set; }

    [Index(4)]
    public string? DestAirportCode { get; set; }

    [Index(5)]
    public int? DestAirportId { get; set; }

    [Index(6)]
    public string? Codeshare { get; set; }

    [Index(7)]
    public int Stops { get; set; }

    [Index(8)]
    public string? Equipment { get; set; }
}