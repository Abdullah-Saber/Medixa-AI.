using Microsoft.AspNetCore.Mvc;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class DoctorDashboardController : Controller
    {
        // GET: /DoctorDashboard
        public IActionResult Index()
        {
            // TODO: Use Application service to get dashboard data
            // var dashboardData = _doctorService.GetDashboardData();
            // var viewModel = new DoctorDashboardViewModel { ... };
            return View();
        }

        // GET: /DoctorDashboard/Appointments
        public IActionResult Appointments()
        {
            return View();
        }

        // GET: /DoctorDashboard/Patients
        public IActionResult Patients()
        {
            return View();
        }

        // GET: /DoctorDashboard/Results
        public IActionResult Results()
        {
            return View();
        }
    }
}
