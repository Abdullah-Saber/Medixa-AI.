using Microsoft.AspNetCore.Mvc;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class PatientDashboardController : Controller
    {
        // GET: /PatientDashboard
        public IActionResult Index()
        {
            // TODO: Use Application service to get dashboard data
            return View();
        }

        // GET: /PatientDashboard/MyOrders
        public IActionResult MyOrders()
        {
            return View();
        }

        // GET: /PatientDashboard/MyResults
        public IActionResult MyResults()
        {
            return View();
        }

        // GET: /PatientDashboard/History
        public IActionResult History()
        {
            return View();
        }
    }
}
