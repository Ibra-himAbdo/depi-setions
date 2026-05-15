namespace Application.Client;

public class MVCAiController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Ask() => View();

    [HttpGet]
    public IActionResult AskAdvanced() => View();
}
