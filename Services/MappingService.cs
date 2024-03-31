using Route = OpenFlightsRazor.Models.Route;

namespace OpenFlightsRazor.Services;

public class MappingService(DataService _dataService)
{
    public async Task<IList<Route>> GetDirectFlightsByName(string sourceAirportName, string destAirportName)
    {
        var sourceAirportId = await GetAirportIdFromName(sourceAirportName);
        var destAirportId = await GetAirportIdFromName(destAirportName);

        return await GetDirectFlightsById(sourceAirportId, destAirportId);
    }

    public async Task<IList<Route>> GetDirectFlightsByCode(string sourceAirportCode, string destAirportCode)
    {
        var sourceAirportId = await GetAirportIdFromCode(sourceAirportCode);
        var destAirportId = await GetAirportIdFromCode(destAirportCode);

        return await GetDirectFlightsById(sourceAirportId, destAirportId);
    }

    public async Task<IList<Route>> GetDirectFlightsById(int sourceAirportId, int destAirportId)
    {
        var allFlights = await _dataService.GetRoutes();

        return allFlights.Where((r) =>
            r.SourceAirportId == sourceAirportId && r.DestAirportId == destAirportId && r.Stops == 0
        ).ToList();

    }

    public async Task<int> GetAirportIdFromName(string airportName)
    {
        var allAirports = await _dataService.GetAirports();

        var matching = allAirports.Where(a =>
            a.Name?.Equals(airportName, StringComparison.OrdinalIgnoreCase) ?? false
        );

        return matching.FirstOrDefault()?.Id ?? throw new OFRInvalidInputException("Unable to find airport ID from name");
    }

    public async Task<int> GetAirportIdFromCode(string airportCode)
    {
        var allAirports = await _dataService.GetAirports();

        var matching = allAirports.Where(a =>
            a.Iata?.Equals(airportCode, StringComparison.OrdinalIgnoreCase) ?? false
        );

        // Assuming that IATA codes are unique - may not be true
        return matching.FirstOrDefault()?.Id ?? throw new OFRInvalidInputException("Unable to find airport ID from code");
    }
    

    public async Task<IList<string>> GetAirportNames()
    {
        var allAirports = await _dataService.GetAirports();
        return allAirports.Where(a => a.Name != null).Select(a => a.Name!).Distinct().OrderBy(a => a).ToList();
    }

    public async Task<IList<string>> GetAirportCodes()
    {
        var allAirports = await _dataService.GetAirports();
        return allAirports.Where(a => a.Iata != null).Select(a => a.Iata!).Distinct().OrderBy(a => a).ToList();
    }

}