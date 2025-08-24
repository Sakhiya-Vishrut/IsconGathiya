using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    [AuthManager]
    public class AttendanceController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
