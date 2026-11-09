using Demo.BLL.Models;

namespace Demo.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult? HandelErrors(List<Error>? errors)
        {
            if (errors?.Count > 0)
            {
                foreach (Error? error in errors)
                {
                    if (error.Type == ErrorType.NotFound)
                    {
                        return NotFound();
                    }

                    if (error.Type == ErrorType.Validation)
                    {
                        ModelState.AddModelError(error.Code ?? string.Empty, error.Descreiption ?? string.Empty);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Descreiption ?? string.Empty);
                    }
                }
            }

            return null;
        }
    }
}
