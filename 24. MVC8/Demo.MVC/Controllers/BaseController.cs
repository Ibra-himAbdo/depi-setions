using Demo.BLL.Models;

namespace Demo.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult? HandelErrors(List<Error>? errors)
        {
            if (errors is not null && errors.Count > 0)
            {
                foreach (var error in errors ?? [])
                {
                    if (error.Type == ErrorType.NotFound)
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(error.Code ?? string.Empty, error.Descreption ?? string.Empty);
                    }
                }
            }

            return null;
        }
    }
}
