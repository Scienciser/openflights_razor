using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using OpenFlightsRazor.Models;
using Route = OpenFlightsRazor.Models.Route;

namespace OpenFlightsRazor.Services;

public class DataService
{
    private readonly CsvConfiguration _csvConfiguration;
    private readonly CustomSettings _config;
    private IList<Airport>? _airportsCache = null;
    private IList<Route>? _routesCache = null;

    public DataService(IOptions<CustomSettings> customSettings)
    {
        _config = customSettings.Value;


        _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
    }
    
    public async Task<IList<Route>> GetRoutes()
    {
        if (_routesCache == null)
        {
            var path = _config.UseLocalData ? _config.RoutesLocalPath : _config.RoutesUri;
            _routesCache = await GetAndParseCsv<Route>(_config.UseLocalData, path!);
        }
        return _routesCache;
    }
    
    public async Task<IList<Airport>> GetAirports()
    {
        if (_airportsCache == null)
        {
            var path = _config.UseLocalData ? _config.AirportsLocalPath : _config.AirportsUri;
            _airportsCache = await GetAndParseCsv<Airport>(_config.UseLocalData, path!);
        }
        return _airportsCache;
    }

    public async Task<IList<T>> GetAndParseCsv<T>(bool localData, string path)
    {
        string csvString;
        if (localData)
        {
            try
            {
                using var streamReader = new StreamReader(path);
                csvString = await streamReader.ReadToEndAsync();
            }
            catch (Exception e)
            {
                throw new OFRLogicException("Unable to read local CSV data", e);
            }
        }
        else
        {
            try
            {
                using var client = new HttpClient();
                var resp = await client.GetAsync(path);
                csvString = await resp.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new OFRLogicException("Unable to retrieve remote CSV data", e);
            }
        }
        csvString = csvString.Replace("\\N","");

        using var reader = new StringReader(csvString);
        using var csv = new CsvReader(reader, _csvConfiguration);

        IList<T> results;
        try
        {
            results = csv.GetRecords<T>().ToList();
        }
        catch (Exception e)
        {
            throw new OFRLogicException("Unable to parse CSV file", e);
        }

        return results;
    }
}