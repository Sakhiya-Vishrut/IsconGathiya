using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeForm()
        {
            return View();
        }

    }
}
