using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenFlightsRazor.Services;
using Route = OpenFlightsRazor.Models.Route;

public class ResultsModel : PageModel
{
    public IList<Route> Results { get; set; } = new List<Route>();
    private readonly MappingService _svc;

    public ResultsModel(MappingService svc)
    {
        _svc = svc;
    }


public async Task<IActionResult> OnGetAsync(bool byName, string sourceAirport, string destAirport)
{
    Results = byName
        ? await _svc.GetDirectFlightsByName(sourceAirport, destAirport)
        : await _svc.GetDirectFlightsByCode(sourceAirport, destAirport);
    return Page();
}
}