using Medixa_AI.Application.DTOs;
using Medixa_AI.Application.Interfaces;
using Medixa_AI.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class PatientDashboardController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IOrderService _orderService;
        private readonly IResultService _resultService;

        public PatientDashboardController(
            IPatientService patientService,
            IOrderService orderService,
            IResultService resultService)
        {
            _patientService = patientService;
            _orderService = orderService;
            _resultService = resultService;
        }

        // GET: /PatientDashboard
        public async Task<IActionResult> Index()
        {
            var allPatients = await _patientService.GetAllAsync();
            var allOrders = await _orderService.GetAllAsync();
            var allResults = await _resultService.GetAllAsync();

            var viewModel = new PatientDashboardViewModel
            {
                PatientID = Guid.Empty, // TODO: Get from authenticated user
                FullName = "Guest User", // TODO: Get from authenticated user
                TotalOrders = allOrders.Count(),
                PendingResults = allResults.Count(r => r.ResultText == null || r.ResultText == ""),
                CompletedResults = allResults.Count(r => r.ResultText != null && r.ResultText != ""),
                RecentOrders = allOrders
                    .Take(5)
                    .Select(o => new RecentOrder
                    {
                        OrderID = o.OrderID.GetHashCode(), // Use hash code for display
                        OrderDate = o.OrderDate,
                        Status = o.Status.ToString(),
                        TestCount = o.OrderDetails?.Count ?? 0
                    }).ToList(),
                HealthAlerts = new List<HealthAlert>
                {
                    new HealthAlert
                    {
                        Type = "Recommendation",
                        Message = "Annual checkup recommended",
                        Date = DateTime.Now.AddDays(-30),
                        Severity = "Low"
                    }
                }
            };

            return View(viewModel);
        }

        // GET: /PatientDashboard/Patients
        public async Task<IActionResult> Patients()
        {
            var patients = await _patientService.GetAllAsync();
            return View(patients);
        }

        // GET: /PatientDashboard/Orders
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        // GET: /PatientDashboard/Results
        public async Task<IActionResult> Results()
        {
            var results = await _resultService.GetAllAsync();
            return View(results);
        }

        // GET: /PatientDashboard/Orders/{patientId}
        public async Task<IActionResult> PatientOrders(Guid patientId)
        {
            var orders = await _orderService.GetByPatientAsync(patientId);
            return View(orders);
        }
    }
}
