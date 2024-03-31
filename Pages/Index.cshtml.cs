using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenFlightsRazor.Services;
using Route = OpenFlightsRazor.Models.Route;

namespace openflights_razor.Pages;

public class IndexInputs
{
    public string? SourceAirportCode { get; set; }
    public string? DestAirportCode { get; set; }
    public string? SourceAirportName { get; set; }
    public string? DestAirportName { get; set; }
    public string? TESTfield { get; set; }
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly MappingService _svc;

    public List<SelectListItem> AirportNames { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> AirportCodes { get; set; } = new List<SelectListItem>();


    [BindProperty]
    public IndexInputs InputModel { get; set; } = new IndexInputs {};

    public IndexModel(ILogger<IndexModel> logger, MappingService svc)
    {
        _logger = logger;
        _svc = svc;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulateAirports();
        return Page();
    }

    public async Task PopulateAirports()
    {
        AirportNames =
        [
            new SelectListItem { Value = null, Text = "" },
            .. (await _svc.GetAirportNames()).Select(s => new SelectListItem { Value = s, Text = s }).ToList(),
        ];

        AirportCodes = 
        [
            new SelectListItem { Value = null, Text = "" },
            .. (await _svc.GetAirportCodes()).Select(s => new SelectListItem { Value = s, Text = s }).ToList(),
        ];
    }

    public async Task<IActionResult> OnPostFilterByName()
    {
        if (string.IsNullOrEmpty(InputModel.SourceAirportName))
        {
            ModelState.AddModelError("Name", "The departure airport is required.");
        }

        if (string.IsNullOrEmpty(InputModel.DestAirportName))
        {
            ModelState.AddModelError("Name", "The destination airport is required.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateAirports();
            return Page();
        }

        return RedirectToPage("/Results", new { byName = true, sourceAirport = InputModel.SourceAirportName, destAirport = InputModel.DestAirportName });
    }

    public async Task<IActionResult> OnPostFilterByCode()
    {
        if (string.IsNullOrEmpty(InputModel.SourceAirportCode))
        {
            ModelState.AddModelError("Name", "The departure airport is required.");
        }

        if (string.IsNullOrEmpty(InputModel.DestAirportCode))
        {
            ModelState.AddModelError("Name", "The destination airport is required.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateAirports();
            return Page();
        }

        return RedirectToPage("/Results", new { byName = false, sourceAirport = InputModel.SourceAirportCode, destAirport = InputModel.DestAirportCode });
    }
}
