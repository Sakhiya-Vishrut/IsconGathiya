using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
