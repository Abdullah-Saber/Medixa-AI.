using Microsoft.AspNetCore.Mvc;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class StaffDashboardController : Controller
    {
        // GET: /StaffDashboard
        public IActionResult Index()
        {
            // TODO: Use Application service to get dashboard data
            return View();
        }

        // GET: /StaffDashboard/Orders
        public IActionResult Orders()
        {
            return View();
        }

        // GET: /StaffDashboard/LabTests
        public IActionResult LabTests()
        {
            return View();
        }

        // GET: /StaffDashboard/Reports
        public IActionResult Reports()
        {
            return View();
        }
    }
}
