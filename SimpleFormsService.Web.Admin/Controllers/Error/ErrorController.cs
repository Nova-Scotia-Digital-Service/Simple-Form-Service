using Microsoft.AspNetCore.Mvc;

namespace SimpleFormsService.Web.Admin.Controllers.Error
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();           
        }
    }
}
