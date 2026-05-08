namespace Application.Client;

public class CascadingController : Controller
{
    private static readonly List<State> StatesList = new()
    {
        new State { Id = 1, Name = "Cairo" },
        new State { Id = 2, Name = "Giza" },
        new State { Id = 3, Name = "Alexandria" }
    };

    private static readonly List<City> CitiesList = new()
    {
        new City { Id = 1, StateId = 1, Name = "Maadi" },
        new City { Id = 2, StateId = 1, Name = "Nasr City" },
        new City { Id = 3, StateId = 1, Name = "Heliopolis" },
        new City { Id = 4, StateId = 2, Name = "6th of October" },
        new City { Id = 5, StateId = 2, Name = "Sheikh Zayed" },
        new City { Id = 6, StateId = 2, Name = "Dokki" },
        new City { Id = 7, StateId = 3, Name = "Sidi Gaber" },
        new City { Id = 8, StateId = 3, Name = "Stanley" },
        new City { Id = 9, StateId = 3, Name = "Montaza" }
    };

    public IActionResult Index()
    {
        ViewBag.States = StatesList;
        return View();
    }

    [HttpGet]
    public IActionResult GetCities(int stateId)
    {
        var filteredCities = CitiesList.Where(c => c.StateId == stateId).ToList();
        return Json(filteredCities);
    }
}

public class State 
{ 
    public int Id { get; set; } 
    public string Name { get; set; } 
}

public class City 
{ 
    public int Id { get; set; } 
    public int StateId { get; set; } 
    public string Name { get; set; } 
}
